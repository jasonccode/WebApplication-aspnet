using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication_aspnet.Models;

namespace WebApplication_aspnet.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Esto le dice a Entity Framework que cree una tabla llamada "Users"
        // basada en tu modelo 'User'
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}