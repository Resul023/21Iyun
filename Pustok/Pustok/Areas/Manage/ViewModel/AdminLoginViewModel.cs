using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.ViewModel
{
    public class AdminLoginViewModel
    {
        [Required]
        [MaxLength(25)]
        [MinLength(6)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
