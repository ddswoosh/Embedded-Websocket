using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Entities;
using Server.Utils;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Server.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<IISOptions>(options =>
{
    options.ForwardClientCertificate = false;
});
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<PinInterface, PinLive>();
builder.Services.AddSingleton<AWSSecrets>();

builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    );

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
