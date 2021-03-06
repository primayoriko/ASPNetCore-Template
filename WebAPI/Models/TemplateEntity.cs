﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class TemplateEntity
    {
        // Insert class field here, example
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { set; get; }

        [Required]
        public string Name { set; get; }

        // In case have relation with other table
        public Template2Entity Template2 { set; get; }
        
    }
}
