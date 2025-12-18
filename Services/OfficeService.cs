using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication_aspnet.Data;
using WebApplication_aspnet.Interfaces;
using WebApplication_aspnet.Models;

namespace WebApplication_aspnet.Services
{
    public class OfficeService : IOfficeService
    {
        public readonly AppDbContext _context;

        public OfficeService(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Office>> GetAllAsync()
        {
            var offices = _context.Offices.ToListAsync();
            return offices;
        }

        public async Task<Office?> GetByIdAsync(int id)
        {
            var office = await _context.Offices.FindAsync(id);
            return office;
        }

        public async Task AddAsync(Office office)
        {
            await _context.Offices.AddAsync(office);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Office office)
        {
            _context.Offices.Update(office);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var office = _context.Offices.Find(id);
            if (office != null)
            {
                _context.Offices.Remove(office);
                await _context.SaveChangesAsync();
            }
        }
    }
}