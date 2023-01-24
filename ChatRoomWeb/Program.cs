using Microsoft.EntityFrameworkCore;
using ChatRoomWeb.Services;
using ChatRoomWeb.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using System.Security.Authentication;
using ChatRoomWeb.Middleware;
using Flurl.Http.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IUserManagementService, UserManagementService>();
builder.Services.AddSingleton<IUserManagementRepository, UserManagementRepository>();
builder.Services.AddSingleton<ITestService, TestService>();
builder.Services.AddSingleton<ITestRepository, TestRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDefaultFiles();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseMiddleware<JWTMiddleware>();
app.Run();