using Enterprise.Api.Rest;
using Enterprise.Application.Filters;
using HC.ServiceDefaults;
using HC.UserAccounts.Api.Rest.Endpoints;
using HC.UserAccounts.Application.Write;
using HC.UserAccounts.EventHandlers;
using HC.UserAccounts.Jobs;
using HC.UserAccounts.Persistence.Context;
using HC.UserAccounts.Persistence.Write;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEnterpriseRestApi(builder.Configuration);

builder.Services.AddUserAccountsPersistenceContext(builder.Configuration);
builder.Services.AddUserAccountsPersistenceWriteLayer();
builder.Services.AddUserAccountsApplicationWriteLayer();
builder.Services.AddJobs();
builder.AddEventHandlers(builder.Configuration);
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

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapUserAccountsEndpoints();

app.Run();