using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODataAPI.Models
{
    public class Template2Class
    {
        [Key]
        public int Grade{ set; get; }

        // In case have relation with other table add this
        public List<TemplateClass> Template { set; get; }

    }
}
