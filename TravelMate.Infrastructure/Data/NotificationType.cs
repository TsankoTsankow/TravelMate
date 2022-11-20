using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMate.Infrastructure.Data
{
    public class NotificationType
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } = null!;
    }
}
