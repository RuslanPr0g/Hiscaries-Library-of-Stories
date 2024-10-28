using FluentValidation.AspNetCore;
using HC.API.ApiEndpoints;
using HC.Application;
using HC.Application.Extensions;
using HC.Application.Filters;
using HC.Application.Options;
using HC.Application.Users.Command;
using HC.Persistence.Read.Extensions;
using HC.Persistence.Write.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceWriteLayer(builder.Configuration);
builder.Services.AddPersistenceReadLayer(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddSerilog();

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(SaveChangesPipelineBehavior<,>));

builder.Services.AddMediatR(cfg =>
{
    // TODO: it takes multiple assemblies, maybe separate read and write projects into two?
    cfg.RegisterServicesFromAssemblies(typeof(RegisterUserCommandHandler).Assembly);
});

builder.Services.AddLogging();

builder.Services
    .AddControllers(options => { options.Filters.Add<ValidationFilter>(); })
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.DictionaryKeyPolicy = null;
});

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddOptions<DbConnectionStrings>()
    .Bind(builder.Configuration.GetSection("ConnectionStrings"));

var jwtSettings = new JwtSettings();
builder.Configuration.Bind(nameof(jwtSettings), jwtSettings);

var saltSettings = new SaltSettings();
builder.Configuration.Bind(nameof(saltSettings), saltSettings);
builder.Services.AddSingleton(saltSettings);

builder.Services.AddJwtBearerSupportAlongWithSwaggerSupport(jwtSettings);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HC.API v1"));
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapUserEndpoints();

app.MapStoryEndpoints();

app.Run();
