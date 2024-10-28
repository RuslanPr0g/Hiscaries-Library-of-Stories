using FluentValidation.AspNetCore;
using HC.API.ApiEndpoints;
using HC.Application.Common;
using HC.Application.Filters;
using HC.Application.Users.Command;
using HC.Application.Users.Queries;
using HC.Persistence.Read;
using HC.Persistence.Write;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationCommonLayer();
builder.Services.AddApplicationReadLayer();
builder.Services.AddApplicationWriteLayer();
builder.Services.AddPersistenceContext();
builder.Services.AddPersistenceWriteLayer(builder.Configuration);
builder.Services.AddPersistenceReadLayer(builder.Configuration);
builder.Services.AddSerilog();
builder.Services.AddLogging();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(RegisterUserCommandHandler).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(GetUserInfoQueryHandler).Assembly);
});

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(SaveChangesPipelineBehavior<,>));

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
