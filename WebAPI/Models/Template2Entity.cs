﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Template2Entity
    {
        [Key]
        public int Grade{ set; get; }

        // In case have relation with other table add this
        public List<TemplateEntity> Template { set; get; }

    }
}
