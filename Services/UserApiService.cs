using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_aspnet.Interfaces;
using WebApplication_aspnet.Models;

namespace WebApplication_aspnet.Services
{
    public class UserApiService : IUserApiService
    {
        private readonly HttpClient _httpClient;

        // Constructor: Inyectamos la fábrica y pedimos el cliente "ClientApi"
        public UserApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ClientApi");
        }

        // 1. LEER TODOS (GET)
        public async Task<List<User>> GetAllAsync()
        {
            // Como BaseAddress termina en "api/", aquí solo ponemos "User"
            // Resultado: http://localhost:5170/api/User
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<User>>("User");
                return response ?? new List<User>();
            }
            catch (Exception)
            {
                // Si la API falla o está caída, retornamos lista vacía para no romper la vista
                return new List<User>();
            }
        }

        // 2. LEER UNO POR ID (GET)
        public async Task<User?> GetByIdAsync(int id)
        {
            try
            {
                // Resultado: http://localhost:5170/api/User/5
                return await _httpClient.GetFromJsonAsync<User>($"User/{id}");
            }
            catch (HttpRequestException)
            {
                // Si la API devuelve 404 (No encontrado), retornamos null
                return null;
            }
        }

        // 3. CREAR (POST)
        public async Task AddAsync(User user)
        {
            // Enviamos el objeto 'user' como JSON en el cuerpo
            await _httpClient.PostAsJsonAsync("User", user);
        }

        // 4. ACTUALIZAR (PUT)
        public async Task UpdateAsync(User user)
        {
            // Es importante enviar el ID en la URL para cumplir con REST
            // Resultado: PUT http://localhost:5170/api/User/5
            await _httpClient.PutAsJsonAsync($"User/{user.Nid_user}", user);
        }

        // 5. ELIMINAR (DELETE)
        public async Task DeleteAsync(int id)
        {
            // Resultado: DELETE http://localhost:5170/api/User/5
            await _httpClient.DeleteAsync($"User/{id}");
        }
    }

}