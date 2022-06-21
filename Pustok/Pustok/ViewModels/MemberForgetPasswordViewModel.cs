using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.ViewModels
{
    public class MemberForgetPasswordViewModel
    {
        [Required]
        [MaxLength(80)]
        public string Email { get; set; }
    }
}
