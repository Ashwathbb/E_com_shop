using Deptt.API.Controllers;
using Microsoft.OpenApi.Models;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddHttpClient();
builder.Services.AddHttpClient<DepartmentController>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7245"); // Base address for department service
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiGateway", Version = "v1" });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

    app.UseDeveloperExceptionPage();


    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSwagger(); // Enable Swagger middleware
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiGateway V1");
});
app.MapControllers();

app.Run();
