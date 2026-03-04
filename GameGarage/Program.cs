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

// app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.MapDefaultControllerRoute();


app.Run();
