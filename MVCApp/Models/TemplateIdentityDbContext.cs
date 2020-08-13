using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MVCApp.Models
{
    // This template class is optional, and only used when you use Identity service to make authentication in ASP NET Core
    // You can remove it if not needed
    // Used as a template for defining DB Context for accessing and storing data in the database
    public class TemplateIdentityDbContext : IdentityDbContext<TemplateIdentityUser>
    {
        public TemplateIdentityDbContext(DbContextOptions<TemplateIdentityDbContext> opts)
                : base(opts)
        {
            // You can leave it empty if you haven't any additional setup
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // In case you have additional action, input below
        }
    }
}
