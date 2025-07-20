using EmployeeService.Data;
using EmployeeService.Repositories;
using EmployeeService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlite("Data Source=employee.db"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// only need to add services, no need to add repositories
builder.Services.AddScoped<IEmployeeService, EmployeeService.Services.EmployeeService>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
