using Hiscary.Api.Rest;
using Hiscary.Application.Filters;
using Hiscary.Notifications.Api.Rest.Endpoints;
using Hiscary.Notifications.Application.Read;
using Hiscary.Notifications.Application.Write;
using Hiscary.Notifications.EventHandlers;
using Hiscary.Notifications.Jobs;
using Hiscary.Notifications.Persistence.Context;
using Hiscary.Notifications.Persistence.Read;
using Hiscary.Notifications.Persistence.Write;
using Hiscary.Notifications.SignalR;
using Hiscary.Notifications.SignalR.Hubs;
using Hiscary.ServiceDefaults;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEnterpriseRestApi(builder.Configuration);

builder.Services.AddNotificationsPersistenceContext(builder.Configuration);
builder.Services.AddNotificationsPersistenceWriteLayer();
builder.Services.AddNotificationsPersistenceReadLayer();
builder.Services.AddNotificationsApplicationWriteLayer();
builder.Services.AddNotificationsApplicationReadLayer();
builder.Services.AddJobs();
builder.AddEventHandlers(builder.Configuration);
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