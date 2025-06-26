using Hiscary.Shared.Api.Rest;
using Hiscary.Media.EventHandlers;
using Hiscary.Media.FileStorage;
using Hiscary.Media.Images;
using Hiscary.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddSharedRestApi(builder.Configuration);

builder.Services.AddMediaFileStorage();
builder.Services.AddMediaImages();
builder.Services.AddEndpointsApiExplorer();
builder.AddEventHandlers(builder.Configuration);
builder.Services.AddSwaggerGen();

builder.AddAzureBlobClient("azblobs", config => config.DisableHealthChecks = true);

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.Run();
