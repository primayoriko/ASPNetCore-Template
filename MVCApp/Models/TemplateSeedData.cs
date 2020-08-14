using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MVCApp.Models
{
    public class TemplateSeedData
    {
        // Static method for adding data in case your database still empty
        // Note : Very useful for testing purpose
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TemplateDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TemplateDbContext>>()))
            {
                if (context.TemplateClasses.Any() && context.Template2Classes.Any())
                {
                    return;
                }
                else
                {
                    context.Template2Classes.AddRange(
                        new Template2Entity
                        {
                            Grade = 1
                        },
                        new Template2Entity
                        {
                            Grade = 8
                        }
                        // Add again if you want
                    );

                    context.TemplateClasses.AddRange(
                        new TemplateEntity
                        {
                            Id = 12,
                            Name = "Hansel"
                        },
                        new TemplateEntity
                        {
                            Id = 13,
                            Name = "Gretel"
                        }
                        // Add again if you want
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
