var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("hiscary")
    .WithDataVolume("hiscarydbdata")
    .WithPgAdmin()
    .AddDatabase("postgres");
var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithDataVolume("hiscaryrabbitmqdata");
var azBlobs = builder.AddAzureStorage("azstorage")
    .RunAsEmulator(
        (config) =>
            config
                .WithImageTag("latest")
                .WithArgs(
                "azurite",
                "-l",
                "/data",
                "--blobHost",
                "0.0.0.0",
                "--queueHost",
                "0.0.0.0",
                "--tableHost",
                "0.0.0.0",
                "--debug",
                "path/debug.log")
                .WithDataVolume()
    )
    .AddBlobs("azblobs");

var useraccounts = builder.AddProject<Projects.HC_UserAccounts_Api_Rest>("hc-useraccounts-api-rest")
    .WithJwtAndSaltSettings(builder.Configuration)
    .WithHttpsEndpoint(name: "rest", port: 7010, targetPort: 7010, isProxied: false)
    .WithReference(postgres)
    .WithReference(rabbitmq)
    .WithReference(azBlobs);
var notifications = builder.AddProject<Projects.HC_Notifications_Api_Rest>("hc-notifications-api-rest")
    .WithJwtAndSaltSettings(builder.Configuration)
    .WithHttpsEndpoint(name: "rest", port: 7011, targetPort: 7011, isProxied: false)
    .WithReference(postgres)
    .WithReference(rabbitmq)
    .WithReference(azBlobs);
var platformusers = builder.AddProject<Projects.HC_PlatformUsers_Api_Rest>("hc-platformusers-api-rest")
    .WithJwtAndSaltSettings(builder.Configuration)
    .WithHttpsEndpoint(name: "rest", port: 7012, targetPort: 7012, isProxied: false)
    .WithReference(postgres)
    .WithReference(rabbitmq)
    .WithReference(azBlobs);
var stories = builder.AddProject<Projects.HC_Stories_Api_Rest>("hc-stories-api-rest")
    .WithJwtAndSaltSettings(builder.Configuration)
    .WithHttpsEndpoint(name: "rest", port: 7013, targetPort: 7013, isProxied: false)
    .WithReference(postgres)
    .WithReference(rabbitmq)
    .WithReference(azBlobs);
var media = builder.AddProject<Projects.HC_Media_Api_Rest>("hc-media-api-rest")
    .WithJwtAndSaltSettings(builder.Configuration)
    .WithHttpsEndpoint(name: "rest", port: 7014, targetPort: 7014, isProxied: false)
    .WithReference(rabbitmq)
    .WithReference(azBlobs);

builder.AddProject<Projects.HC_LocalApiGateway>("hc-localapigateway")
    .WithHttpsEndpoint(name: "apigateway", port: 5001, targetPort: 5001, isProxied: false)
    .WithReference(useraccounts)
    .WithReference(notifications)
    .WithReference(platformusers)
    .WithReference(stories)
    .WithReference(media);

builder.Build().Run();
