
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services_For_BFF;
using Shop_BFF.DTOs;
using Shop_BFF.Helpers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




builder.Services.AddHttpClient("ShopApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7088"); // Base URL for Shop.API
});
builder.Services.AddHttpClient("DepartmentApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7245"); // Base URL for Shop.API
});
builder.Services.AddScoped<IDepartment_BFF_Services, Department_BFF_Services>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Configure Services
// Load app settings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Jwt"));

// Configure JWT Authentication
// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();
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
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure authentication is added to the pipeline
app.UseAuthorization();  // Ensure authorization is added to the pipeline

app.MapControllers();

app.Run();

