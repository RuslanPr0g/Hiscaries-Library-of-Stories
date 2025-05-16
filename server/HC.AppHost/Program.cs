var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("hiscary")
    .WithVolume(target: "/var/opt/pssql")
    .WithHttpsEndpoint(name: "db", port: 7009, targetPort: 7009, isProxied: false)
    .WithPgAdmin()
    .AddDatabase("postgres");
var rabbitmq = builder.AddRabbitMQ("rabbitmq");

var useraccounts = builder.AddProject<Projects.HC_UserAccounts_Api_Rest>("hc-useraccounts-api-rest")
    .WithJwtAndSaltSettings(builder.Configuration)
    .WithHttpsEndpoint(name: "rest", port: 7010, targetPort: 7010, isProxied: false)
    .WithReference(postgres)
    .WithReference(rabbitmq);
var notifications = builder.AddProject<Projects.HC_Notifications_Api_Rest>("hc-notifications-api-rest")
    .WithJwtAndSaltSettings(builder.Configuration)
    .WithHttpsEndpoint(name: "rest", port: 7011, targetPort: 7011, isProxied: false)
    .WithReference(postgres)
    .WithReference(rabbitmq);
var platformusers = builder.AddProject<Projects.HC_PlatformUsers_Api_Rest>("hc-platformusers-api-rest")
    .WithJwtAndSaltSettings(builder.Configuration)
    .WithHttpsEndpoint(name: "rest", port: 7012, targetPort: 7012, isProxied: false)
    .WithReference(postgres)
    .WithReference(rabbitmq);
var stories = builder.AddProject<Projects.HC_Stories_Api_Rest>("hc-stories-api-rest")
    .WithJwtAndSaltSettings(builder.Configuration)
    .WithHttpsEndpoint(name: "rest", port: 7013, targetPort: 7013, isProxied: false)
    .WithReference(postgres)
    .WithReference(rabbitmq);

builder.AddProject<Projects.HC_LocalApiGateway>("hc-localapigateway")
    .WithHttpsEndpoint(name: "apigateway", port: 5001, targetPort: 5001, isProxied: false)
    .WithReference(useraccounts)
    .WithReference(notifications)
    .WithReference(platformusers)
    .WithReference(stories);

builder.Build().Run();
