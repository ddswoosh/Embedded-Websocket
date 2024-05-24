using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Server.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<PinInterface, PinLive>();
builder.Services.AddSingleton<User>();
// builder.Services.AddSingleton<Authorization>();
// builder.Services.AddAuthorization(options => {
//     options.AddPolicy("Auth", policy => policy.Requirements.Add(new Authorization(null)));
// });


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
