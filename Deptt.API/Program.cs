using Dept.DataAcess.Dto;
using Dept.DataAcess.Models;
using Dept.Repository.Repositories;
using Dept.Services.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);//The CreateBuilder() method setup the internal web server which is Kestrel. It also specifies the
                                                 //content root and read application settings file appsettings.json.

// Add services to the container.
//The builder object has the Services() method which can be used to add services to the dependency injection container.
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
    app.UseSwagger();//use means configure the middlevare(
    app.UseSwaggerUI();//Use" word means it configures the middleware.
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
/**The MapControllerRoute() defines the default route pattern that specifies which controller, action,
 * and optional route parameters should be used to handle incoming requests.**/

app.Run();
/**
 * Finally, app.run() method runs the application,start listening the incomming request. 
 *  It turns a console application into an MVC application based on the provided configuration.

So, program.cs contains codes that sets up all necessary infrastructure for your application.
 * ***/