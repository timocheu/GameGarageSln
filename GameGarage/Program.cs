using Microsoft.EntityFrameworkCore;
using GameGarage.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Makes the DbContext
builder.Services.AddDbContext<GameGarageDbContext>( options =>
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:GameGarageConnection"]
        ));

// Repository is made by using the context
builder.Services.AddScoped<IGarageRepository, EFGarageRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute("home", "/",
        new { Controller = "Home", action = "Index" });

app.MapControllerRoute(
        name: "app",
        pattern: "app/{id}",
        defaults: new { Controller = "Game", action = "Details" });

app.MapControllerRoute(
        name: "category_paged",
        pattern: "category/{categoryInput}/page/{currentPage}",
        defaults: new { Controller = "Catalog", action = "CategorySearch" });

app.MapControllerRoute(
        name: "tag_paged",
        pattern: "tag/{tagInput}/page/{currentPage}",
        defaults: new { Controller = "Catalog", action = "TagSearch" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
