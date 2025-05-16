using Enterprise.Api.Rest;
using Enterprise.Application.Filters;
using HC.ServiceDefaults;
using HC.Stories.Api.Rest.Endpoints;
using HC.Stories.Application.Read;
using HC.Stories.Application.Write;
using HC.Stories.Jobs;
using HC.Stories.Persistence.Context;
using HC.Stories.Persistence.Read;
using HC.Stories.Persistence.Write;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEnterprise(builder.Configuration);

builder.Services.AddStoriesPersistenceContext(builder.Configuration);
builder.Services.AddStoriesPersistenceWriteLayer();
builder.Services.AddStoriesPersistenceReadLayer();
builder.Services.AddStoriesApplicationWriteLayer();
builder.Services.AddStoriesApplicationReadLayer();
builder.Services.AddJobs();
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

app.MapStoriesEndpoints();

app.Run();