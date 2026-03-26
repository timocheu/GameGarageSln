using Microsoft.EntityFrameworkCore;
using GameGarage.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Makes the DbContext
builder.Services.AddDbContext<GameGarageDbContext>( options =>
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:GameGarageConnection"]
        ));

// Repository is made by using the context
builder.Services.AddScoped<IGarageRepository, EFGarageRepository>();
builder.Services.AddScoped<GameGarage.Infrastructure.IAuditService, GameGarage.Infrastructure.EFAuditService>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<GameGarageDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("home", "/",
        new { Controller = "Home", action = "Index" });

app.MapControllerRoute(
        name: "app",
        pattern: "app/{id}",
        defaults: new { Controller = "Game", action = "Details" });

app.MapControllerRoute(
        name: "category_paged",
        pattern: "category/{category}/page/{currentPage}",
        defaults: new { Controller = "Catalog", action = "Index" });

app.MapControllerRoute(
        name: "tag_paged",
        pattern: "tag/{searchString}/page/{currentPage}",
        defaults: new { Controller = "Catalog", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
