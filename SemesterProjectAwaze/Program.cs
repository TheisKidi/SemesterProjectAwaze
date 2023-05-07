using AwazeLib.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SemesterProjectAwaze.Services;


var builder = WebApplication.CreateBuilder(args);

// Set cookie expiration and sliding expiration
/*builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(14); // Change the value according to your needs
    options.SlidingExpiration = true;
});
builder.Services.AddDistributedMemoryCache();
*/
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



// Add this line if it's not already present
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IGenericRepositoryService<HouseOwner>, HouseOwnerRepositoryServiceDB>();
builder.Services.AddSingleton<IGenericRepositoryService<Guest>, GuestRepositoryServiceDB>();
builder.Services.AddSingleton<IGenericRepositoryService<Property>, PropertyRepositoryServiceDB>();
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
