var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationCommonLayer();
builder.Services.AddApplicationReadLayer();
builder.Services.AddApplicationWriteLayer();
builder.Services.AddPersistenceContext(builder.Configuration);
builder.Services.AddPersistenceWriteLayer(builder.Configuration);
builder.Services.AddPersistenceReadLayer(builder.Configuration);
builder.Services.AddJobs();
builder.Services.AddEventHandlers();
builder.Services.AddSerilog();
builder.Services.AddLogging();
builder.Services.AddHiscariesSignalR();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapUserAccountEndpoints();
app.MapPlatformUserEndpoints();

app.MapStoryEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapContentGenerationEndpoints();
}

app.MapHub<UserNotificationHub>("/hubs/usernotifications");

app.Run();
