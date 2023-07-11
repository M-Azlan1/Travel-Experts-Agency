using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelExpertsData;
using Microsoft.AspNetCore.Identity;




var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TravelExpertsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TravelExpertsContext") ?? throw new InvalidOperationException("Connection string 'TravelExpertsContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(opt => opt.LoginPath="/Account/Login");
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePages(); // add for user friendly error messages for 404, 403 errors
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
