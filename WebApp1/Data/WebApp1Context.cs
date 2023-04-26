using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data.Model;

namespace WebApp1.Data
{
    public class WebApp1Context : DbContext
    {
        public WebApp1Context(DbContextOptions<WebApp1Context> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Batch>()
                    .Property(b => b.BusinessUnitName)
                    .IsRequired();
            modelBuilder.Entity<Users>()
                    .Property(b => b.Name)
                    .IsRequired();
            modelBuilder.Entity<Groups>()
                    .Property(b => b.Name)
                    .IsRequired();
            modelBuilder.Entity<BatchAttribute>(e => { 
                e.Property(b=>b.Key).IsRequired();
                e.Property(b=>b.Value).IsRequired();
            });

        }
        public DbSet<Batch> Batch { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<BatchAttribute> BatchAttributes { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<BatchFiles> BatchFiles { get; set; }

    }
}
