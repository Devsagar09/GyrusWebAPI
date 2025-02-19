using GyrusWebAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connect to the database
builder.Services.AddDbContext<DemoAPICoreDatabaseContext>(u => u.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));

//CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
               builder =>
               {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
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

app.UseAuthentication();

//use CORS
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
