using Buissnes.Abstract;
using Buissnes.Concrete;
using Core.DataAccess;
using Core.Helpers.JWT;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test01", Version = "v1" });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "JWT Authorization header using the Bearer scheme."
//    });
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//                {
//                    {
//                          new OpenApiSecurityScheme
//                          {
//                              Reference = new OpenApiReference
//                              {
//                                  Type = ReferenceType.SecurityScheme,
//                                  Id = "Bearer"
//                              }
//                          },
//                         new string[] {}
//                    }
//                });
//});


#region JWT

var appSettingsSection = builder.Configuration.GetSection("Appsettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.SecurityKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion



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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
