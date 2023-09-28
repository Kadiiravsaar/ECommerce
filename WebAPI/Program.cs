using Buissnes.Abstract;
using Buissnes.Concrete;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(EfBaseRepository<,>));


//builder.Services.AddDbContext<AppDbContext>(x =>
//{
//    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"), option =>
//    {
//        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
//    });
//});



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
