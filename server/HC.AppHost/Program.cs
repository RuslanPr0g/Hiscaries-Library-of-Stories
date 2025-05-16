var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.HC_UserAccounts_Api_Rest>("hc-useraccounts-api-rest");

builder.AddProject<Projects.HC_Stories_Api_Rest>("hc-stories-api-rest");

builder.AddProject<Projects.HC_PlatformUsers_Api_Rest>("hc-platformusers-api-rest");

builder.AddProject<Projects.HC_Notifications_Api_Rest>("hc-notifications-api-rest");

builder.Build().Run();
