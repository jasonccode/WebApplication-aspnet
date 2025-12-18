using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApplication_aspnet.Data;
using WebApplication_aspnet.Interfaces;
using WebApplication_aspnet.Models;

namespace WebApplication_aspnet.Services
{
    public class CityService : ICityService
    {
        public readonly AppDbContext _context;

        public CityService(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<City>> GetAllAsync()
        {
            var cities = _context.Cities.ToListAsync();
            return cities;
        }

        public async Task<City?> GetByIdAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            return city;
        }

        public async Task AddAsync(City city)
        {
            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(City city)
        {
            _context.Cities.Update(city);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
        }
    }
}