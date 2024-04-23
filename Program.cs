using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Galbaat.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using Galbaat.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GalbaatContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("GalbaatContext") ?? throw new InvalidOperationException("Connection string 'GalbaatContext' not found.")));
var connectionString = builder.Configuration.GetConnectionString("GalbaatIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'GalbaatIdentityDbContextConnection' not found.");

builder.Services.AddDbContext<GalbaatIdentityDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<GalbaatIdentityDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
