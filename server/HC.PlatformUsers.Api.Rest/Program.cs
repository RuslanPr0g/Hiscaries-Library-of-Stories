using Enterprise.Application.Filters;
using HC.PlatformUsers.Api.Rest.Endpoints;
using HC.PlatformUsers.Application.Read;
using HC.PlatformUsers.Application.Write;
using HC.PlatformUsers.EventHandlers;
using HC.PlatformUsers.Jobs;
using HC.PlatformUsers.Persistence.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddPlatformUsersPersistenceContext(builder.Configuration);
builder.Services.AddPlatformUsersApplicationWriteLayer();
builder.Services.AddPlatformUsersApplicationReadLayer();
builder.Services.AddJobs();
builder.Services.AddEventHandlers();
builder.Services.AddSerilog();
builder.Services.AddLogging();

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

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapPlatformUsersEndpoints();

app.Run();