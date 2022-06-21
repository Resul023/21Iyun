using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Authors
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:100,ErrorMessage ="Length must be less than 100")]
        public string FullName { get; set; }
        public int Age { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }


        public List<Books> books { get; set; }
        [ForeignKey("ModifiedBy")]
        public AppUser AppUser { get; set; }
    }
}
