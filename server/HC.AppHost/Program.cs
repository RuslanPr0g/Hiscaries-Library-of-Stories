var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .AddDatabase("hiscary");
var rabbitmq = builder.AddRabbitMQ("rabbitmq");

var useraccounts = builder.AddProject<Projects.HC_UserAccounts_Api_Rest>("hc-useraccounts-api-rest")
    .WithReference(postgres)
    .WithReference(rabbitmq);
var notifications = builder.AddProject<Projects.HC_Notifications_Api_Rest>("hc-notifications-api-rest")
    .WithReference(postgres)
    .WithReference(rabbitmq);
var platformusers = builder.AddProject<Projects.HC_PlatformUsers_Api_Rest>("hc-platformusers-api-rest")
    .WithReference(postgres)
    .WithReference(rabbitmq);
var stories = builder.AddProject<Projects.HC_Stories_Api_Rest>("hc-stories-api-rest")
    .WithReference(postgres)
    .WithReference(rabbitmq);

builder.Build().Run();
