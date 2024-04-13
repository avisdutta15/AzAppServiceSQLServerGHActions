using App.Api.Endpoints;
using App.Data;
using App.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//The following are moved to App.Services.ServiceCollectionExtensions.
//Instead the extension methods AddAppServices() and AddPostgresDbContext<T>() are added
//builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    var dataSource =
//        new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("AppDb"))
//        .Build();
//    options.UseNpgsql(dataSource);
//});

builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddPostgresDbContext<AppDbContext>(builder.Configuration.GetConnectionString("AppDb"));


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCustomEndpoints();

app.Run();