using Identity.API.Configuration;
using Identity.Common.Exceptions;
using Identity.Common.Mappers;
using Identity.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddCors(option =>
//{
//    option.AddPolicy("MyCors", p => p.AllowAnyOrigin()
//                                    .AllowAnyMethod()
//                                    .AllowAnyHeader());
//});
// Đăng ký các dịch vụ
builder.Services.AddMyDependencyGroup(builder.Configuration);

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

// Use the custom global exception handling middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
