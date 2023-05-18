using AwazeLib.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SemesterProjectAwaze.Services;


var builder = WebApplication.CreateBuilder(args);

// Add this line if it's not already present
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IGenericRepositoryService<HouseOwner>, HouseOwnerRepositoryServiceDB>();
builder.Services.AddSingleton<IGenericRepositoryService<Guest>, GuestRepositoryServiceDB>();
builder.Services.AddSingleton<IGenericRepositoryService<Property>, PropertyRepositoryServiceDB>();
builder.Services.AddSingleton <FavoriteRepositoryService>();
builder.Services.AddSingleton <OrderRepositoryServiceDB>();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
