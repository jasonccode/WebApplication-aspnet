using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_aspnet.Models;

namespace WebApplication_aspnet.Interfaces
{
    public interface IOfficeService
    {
        Task<List<Office>> GetAllAsync();
        Task<Office?> GetByIdAsync(int id);
        Task AddAsync(Office office);
        Task UpdateAsync(Office office);
        Task DeleteAsync(int id);
    }
}
