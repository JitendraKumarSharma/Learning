var builder = DistributedApplication.CreateBuilder(args);

var productApi = builder.AddProject<Projects.MicroserviceKafka_ProductService>("apiservice-product").WithUrlForEndpoint("https", url => url.Url = "/swagger");
;
var orderApi = builder.AddProject<Projects.MicroserviceKafka_OrderService>("apiservice-order").WithUrlForEndpoint("https", url => url.Url = "/swagger");
;

builder.AddProject<Projects.MicroserviceKafka_Web>("webfrontend")
    .WithReference(productApi)
    .WithReference(orderApi);

builder.Build().Run();
