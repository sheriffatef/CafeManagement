using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class CaffeDbContext : IdentityDbContext<User>
    {
        public CaffeDbContext(DbContextOptions<CaffeDbContext> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; } = null!;
        public DbSet<Orders> Orders { get; set; } = null!;
        public DbSet<Tables> Tables { get; set; } = null!;
        public DbSet<OrderItems> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyRef).Assembly);
        }
    }
}
