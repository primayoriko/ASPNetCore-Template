using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MVCApp.Models
{
    public class TemplateIdentityDbContext : IdentityDbContext<TemplateIdentityDbContext>
    {
        public TemplateIdentityDbContext(DbContextOptions<TemplateIdentityDbContext> opts)
                : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
