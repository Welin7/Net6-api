using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {

        }
    
        public DbSet<Message> Message { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }   
    
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(getConnectionString());
                base.OnConfiguring(optionsBuilder);
            }
        }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(u => u.Id);
            base.OnModelCreating(builder);
        }

        
        public string getConnectionString()
        {
            return "Server=WELIN\\SQL2019,1433;Database=ApiNet6;User Id=sa;Password=medsys;";
        }   
    }
}
