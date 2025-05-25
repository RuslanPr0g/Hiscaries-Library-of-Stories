using Enterprise.Api.Rest;
using HC.Media.EventHandlers;
using HC.Media.FileStorage;
using HC.Media.Images;
using HC.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEnterprise(builder.Configuration);

builder.Services.AddMediaFileStorage();
builder.Services.AddMediaImages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEventHandlers(builder.Configuration);
builder.Services.AddSwaggerGen();

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
