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
            // You can leave it blank
        }

        //Insert all of db sets from your entities here, for example
        public DbSet<TemplateEntity> TemplateClasses { get; set; }
        public DbSet<Template2Entity> Template2Classes { get; set; }

        // This is optional method, in case you want define relation of tables yourself, 
        // with  Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // For define Pimary key
            modelBuilder.Entity<Template2Entity>()
                .HasKey(s => s.Grade);

            modelBuilder.Entity<TemplateEntity>()
                .HasKey(s => s.Id);

            //For define relation, in case youre using use many entity
            modelBuilder.Entity<TemplateEntity>()
                .HasOne(s => s.Template2)
                .WithMany(c => c.Template);
                // If TemplateClass have foreign key from Template2Class, add line below
                //.HasForeignKey(s => new { s.Grade, s.ClassNumber });
        }
    }

    /*
     * How to Migrate DB(basic concept/step):
     
        1. Open PMC (Package Manager Console)
        2. Execute `Add-Migration <write a name here as a progress>`
        3. Execute `Update-Database`
        4. If you change model or your DbContext reapply 1st - 3rd step
        5. To undo the change, Execute `Remove-Migration < write a previous/current name of migration >`, 
            the DB condition should came back into a condition where that migration hasn't applied
        6. For more detailed case, setting, or problems you can always search in docs in Microsoft or other websites
     */
}
