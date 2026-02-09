using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication_aspnet.Services;

namespace WebApplication_aspnet.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly AuthService _authService;

        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet] // Esta es la que responde cuando entras a la URL /Login
        public IActionResult IniciarSesion()
        {
            // Si el usuario ya está autenticado, lo mandamos al Home directamente
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            // 1. Intentar loguearse en la API
            string token = await _authService.LoginEnApi(correo, clave);

            if (token == null)
            {
                ViewData["Mensaje"] = "Credenciales incorrectas en la API";
                return View();
            }

            // 2. Si el Token es válido, creamos la identidad del usuario en el MVC
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, correo),
                new Claim("JWToken", token) // Guardamos el token dentro de la cookie por si lo necesitamos
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // 3. Firmar la Cookie (Login en el navegador)
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }
    }
}