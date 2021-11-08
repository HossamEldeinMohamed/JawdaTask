using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JawdaTask.Models 
{ 
    public class E_CommerceContext : IdentityDbContext<IdentityUser>
    {
        public E_CommerceContext(DbContextOptions<E_CommerceContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<Category>().HasData(
                new Category {  CategoryId = 1, Description = "Test 1", Name = "Test 1" },
                new Category { CategoryId = 2, Description = "Test 2", Name = "Test 2" });
        }
    }
}
