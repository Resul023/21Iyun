using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]
        public string Key { get; set; }
        [Required]
        [StringLength(maximumLength: 180)]
        public string Value { get; set; }
    }
}
