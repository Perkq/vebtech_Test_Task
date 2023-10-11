using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vebtech_Test_Task.Data;
using vebtech_Test_Task.HTTPErrors;
using vebtech_Test_Task.Repositories;
using vebtech_Test_Task.Repositories.Interfaces;
using vebtech_Test_Task.Services;
using vebtech_Test_Task.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();



builder.Configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<HTTPStatusHandler>();

app.UseAuthorization();

app.MapControllers();

app.Run();
