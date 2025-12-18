using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_aspnet.Models;

namespace WebApplication_aspnet.Interfaces
{
    public interface ICityService
    {
        Task<List<City>> GetAllAsync();
        Task<City?> GetByIdAsync(int id);
        Task AddAsync(City city);
        Task UpdateAsync(City city);
        Task DeleteAsync(int id);
    }
}