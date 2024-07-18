using FluentValidation.AspNetCore;
using HC.Application;
using HC.Application.Common.Extentions;
using HC.Application.Filters;
using HC.Application.Options;
using HC.Application.Users.Command.CreateUser;
using HC.Infrastructure.Extentions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using HC.API.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddServicesServices();
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

builder.Services.AddApiVersioning(options => { options.AssumeDefaultVersionWhenUnspecified = true; });

builder.Services.AddOptions<DbConnectionStrings>()
    .Bind(builder.Configuration.GetSection("ConnectionStrings"));

var jwtSettings = new JwtSettings();
builder.Configuration.Bind(nameof(jwtSettings), jwtSettings);

var saltSettings = new SaltSettings();
builder.Configuration.Bind(nameof(saltSettings), saltSettings);
builder.Services.AddSingleton(saltSettings);

builder.Services.AddJwtBearerBasedSwaggerSupport(jwtSettings);

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

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapUserEndpoints();

app.Run();
