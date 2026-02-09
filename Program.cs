using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using WebApplication_aspnet.Data;
using WebApplication_aspnet.Interfaces;
using WebApplication_aspnet.Services;
using Microsoft.AspNetCore.Authentication.Cookies; // <--- AGREGAR ESTO

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de Base de Datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Registro de Servicios
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IOfficeService, OfficeService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUserApiService, UserApiService>();
builder.Services.AddScoped<AuthService>(); // <--- REGISTRA TU NUEVO SERVICIO DE LOGIN

// 3. Configuración de Autenticación por Cookies (PARA EL MVC)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/IniciarSesion"; // A donde va si no está logueado
        options.AccessDeniedPath = "/Home/Privacy"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

// 4. Configuración de Sesiones (Para guardar el Token JWT si lo necesitas)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 5. Configuración del Cliente HTTP para la API
builder.Services.AddHttpClient("ClientApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5245/api/");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// 6. Configuración del Pipeline (EL ORDEN IMPORTA)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// --- ÁREA DE SEGURIDAD ---
app.UseAuthentication(); // 1. ¿Quién eres? (Lee la cookie)
app.UseAuthorization();  // 2. ¿Qué puedes hacer?
app.UseSession();        // 3. Habilita el uso de TempData y Session
// -------------------------

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();