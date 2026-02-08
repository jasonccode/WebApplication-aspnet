using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using WebApplication_aspnet.Data;
using WebApplication_aspnet.Interfaces;
using WebApplication_aspnet.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IOfficeService, OfficeService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUserApiService, UserApiService>();

// Registra y configura un cliente HTTP con nombre para tu API.
builder.Services.AddHttpClient("ClientApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5245/api/");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});

    
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

app.Run();
