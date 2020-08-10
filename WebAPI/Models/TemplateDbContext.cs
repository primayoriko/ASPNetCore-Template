using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class TemplateDbContext : DbContext
    {
        public TemplateDbContext(DbContextOptions<TemplateDbContext> options)
            : base(options)
        {

        }

        //Your db set here, example
        public DbSet<TemplateClass> TemplateClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // For table relation use FLUENT API Method, pls define here, for example

            // For define Pimary key
            modelBuilder.Entity<Template2Class>()
                .HasKey(s => s.Grade);

            modelBuilder.Entity<TemplateClass>()
                .HasKey(s => s.Id);

            //For define relation, in case youre using use many entity
            modelBuilder.Entity<TemplateClass>()
                .HasOne(s => s.Template2)
                .WithMany(c => c.Template);
                // If TemplateClass have foreign key from Template2Class, add line below
                //.HasForeignKey(s => new { s.Grade, s.ClassNumber });

        }
    }
}
