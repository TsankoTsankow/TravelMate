using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMate.Core.Models.ApplicationUser
{
    public class UserViewModel
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;
    }
}
