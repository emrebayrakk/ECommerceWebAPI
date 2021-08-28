using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Concrete.EntityFramework.Mapping;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Contexts
{
    public class ECommerceWebAPIContext:DbContext
    {
        public ECommerceWebAPIContext(DbContextOptions<ECommerceWebAPIContext> options):base(options)
        {
            
        }

        public ECommerceWebAPIContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString= "Data Source =.\\SQLEXPRESS; Initial Catalog = ECommerceWebAPIDb; Integrated Security = True";
            optionsBuilder.UseSqlServer(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
