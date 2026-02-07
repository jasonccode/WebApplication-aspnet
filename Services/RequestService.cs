using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication_aspnet.Data;
using WebApplication_aspnet.Interfaces;
using WebApplication_aspnet.Models;

namespace WebApplication_aspnet.Services
{
    public class RequestService : IRequestService
    {
        public readonly AppDbContext _context;

        public RequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Request>> GetAllAsync()
        {
            return await _context.Requests.FromSqlRaw("EXEC sp_get_all_requests").ToListAsync();
        }

        public async Task<Request?> GetByIdAsync(int id)
        {
            var pId = new SqlParameter("@Id_request", id);
            var result = await _context.Requests.FromSqlRaw("EXEC sp_get_request_by_id @Id_request", pId).ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task AddAsync(Request request)
        {
            var pTitle = new SqlParameter("@Title", request.Title);
            var pDescription = new SqlParameter("@Description", request.Description);
            var pStatus = new SqlParameter("@Status", request.Status);

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_add_request @Title, @Description, @Status",
                pTitle, pDescription, pStatus);
        }

        public async Task UpdateAsync(Request request)
        {
            var pId_request= new SqlParameter("@Id_request", request.Id_request);
            var pTitle = new SqlParameter("@Title", request.Title);
            var pDescription = new SqlParameter("@Description", request.Description);
            var pStatus = new SqlParameter("@Status", request.Status);

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_update_request @Id_request, @Title, @Description, @Status",
                pId_request, pTitle, pDescription, pStatus);
        }

        public async Task DeleteAsync(int id)
        {
            var pId_request = new SqlParameter("@Id_request", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_delete_request @Id_request", pId_request);
        }
    }
}