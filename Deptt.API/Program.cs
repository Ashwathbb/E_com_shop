using Dept.DataAcess.Dto;
using Dept.DataAcess.Models;
using Dept.Repository.Repositories;
using Dept.Services.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DepartmentDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories from DataAccess project
builder.Services.AddScoped<IDepartmentRepository, DeptRepo>();
builder.Services.AddScoped<IDepartmentService, DeptServices>();


//register customer service
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();

//register the address layers
builder.Services.AddScoped<IAddressRepo, AddressRepo>();
builder.Services.AddScoped<IAddresServices, AddresServices>();
//builder.Services.AddAutoMapper< SimpleCustomerDto,Customer>();
builder.Services.AddControllers();

// Register IDbConnection with the connection string
builder.Services.AddTransient<IDbConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
