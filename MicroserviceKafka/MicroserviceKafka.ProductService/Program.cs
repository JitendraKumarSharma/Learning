
using MicroserviceKafka.ProductService.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer("Data Source=JITENDRA; Initial Catalog=EcommerceProduct; Integrated Security=True; TrustServerCertificate=True"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/swagger");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
