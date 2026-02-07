using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_aspnet.Models;

namespace WebApplication_aspnet.Interfaces
{
    public interface IRequestService
    {
        Task<List<Request>> GetAllAsync();
        Task<Request?> GetByIdAsync(int id);
        Task AddAsync(Request request);
        Task UpdateAsync(Request request);
        Task DeleteAsync(int id);
    }
}