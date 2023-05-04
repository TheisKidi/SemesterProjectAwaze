using AwazeLib.model;
using SemesterProjectAwaze.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IGenericRepositoryService<Property>, PropertyRepositoryService>();
builder.Services.AddSingleton<IGenericRepositoryService<Guest>, GuestRepositoryService>();
builder.Services.AddSingleton<IGenericRepositoryService<HouseOwner>, HouseOwnerRepositoryService>();
builder.Services.AddSingleton<ILoginService, LoginService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
