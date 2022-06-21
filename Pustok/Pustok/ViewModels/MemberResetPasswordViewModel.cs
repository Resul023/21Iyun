using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.ViewModels
{
    public class MemberResetPasswordViewModel
    {
        [Required]
        [MaxLength(35)]
        [MinLength(8)]
        public string Email { get; set; }
        [Required]
        [MaxLength(120)]

        public string Token { get; set; }
        [Required]
        [MaxLength(35)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MaxLength(35)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
