using Microsoft.AspNetCore.Authentication.Cookies;
using WebAPIWithCoreMvc.ApiServices;
using WebAPIWithCoreMvc.ApiServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
 
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(); 

#region httpclient
builder.Services.AddHttpClient<IAuthApiService, AuthApiService>(opt =>
{
    opt.BaseAddress = new Uri("https://localhost:7258/api/");
});

builder.Services.AddHttpClient<IUserApiService, UserApiService>(opt =>
{
    opt.BaseAddress = new Uri("https://localhost:7258/api/");
});
#endregion

#region Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Admin/Auth/Login";
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    opt.SlidingExpiration = true;
    opt.Cookie.Name = "mvccookie";
});

#endregion


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        areaName:"Admin",
      name: "Admin",
      pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
