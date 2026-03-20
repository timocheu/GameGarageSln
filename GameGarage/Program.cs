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
app.MapControllerRoute("app", "app/{id}",
        new { Controller = "Game", action = "Details" });
app.MapControllerRoute("category", "category/{category}",
        new { Controller = "Catalog", action = "Category" });


app.Run();
