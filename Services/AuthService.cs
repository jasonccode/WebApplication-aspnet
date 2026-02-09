using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_aspnet.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> LoginEnApi(string correo, string clave)
        {
            var objetoLogin = new { Correo = correo, Clave = clave };
            var response = await _http.PostAsJsonAsync("api/Acceso/Login", objetoLogin);

            if (response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadFromJsonAsync<dynamic>();
                return resultado.GetProperty("token").GetString(); // Obtenemos el JWT
            }
            return null;
        }
    }
}