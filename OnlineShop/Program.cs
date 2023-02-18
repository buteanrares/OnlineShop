using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShop.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<OnlineShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using var scopes = app.Services.CreateScope();
var services = scopes.ServiceProvider;
DbInitializer.Seed(services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<OnlineShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineShop")));
        builder.Services.AddDbContextPool<AuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineShopIdentity")));
        builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.MapRazorPages();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
