using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.ViewModels
{
    public class MemberLoginViewModel
    {
        [Required]
        [MaxLength(30)]
        [MinLength(6)]
        public string LoginUserName { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
    }
}
