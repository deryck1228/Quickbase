using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickbase
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldIdAttribute : Attribute
    {
        [Required]
        public string Fid { get; set; }
        [Required]
        public string Type { get; set; } 

        public FieldIdAttribute(string fid, string type)
        {
            Type = type;
            Fid = fid;
        }
    }
}
