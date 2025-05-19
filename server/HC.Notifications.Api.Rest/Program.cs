using Enterprise.Api.Rest;
using Enterprise.Application.Filters;
using HC.Notifications.Api.Rest.Endpoints;
using HC.Notifications.Application.Read;
using HC.Notifications.Application.Write;
using HC.Notifications.EventHandlers;
using HC.Notifications.Jobs;
using HC.Notifications.Persistence.Context;
using HC.Notifications.Persistence.Read;
using HC.Notifications.Persistence.Write;
using HC.Notifications.SignalR;
using HC.Notifications.SignalR.Hubs;
using HC.ServiceDefaults;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEnterprise(builder.Configuration);

builder.Services.AddNotificationsPersistenceContext(builder.Configuration);
builder.Services.AddNotificationsPersistenceWriteLayer();
builder.Services.AddNotificationsPersistenceReadLayer();
builder.Services.AddNotificationsApplicationWriteLayer();
builder.Services.AddNotificationsApplicationReadLayer();
builder.Services.AddJobs();
builder.Services.AddEventHandlers(builder.Configuration);
builder.Services.AddSerilog();
builder.Services.AddLogging();
builder.Services.AddNotificationsSignalR();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapNotificationsEndpoints();

app.MapHub<UserNotificationHub>("/hubs/usernotifications");

app.Run();