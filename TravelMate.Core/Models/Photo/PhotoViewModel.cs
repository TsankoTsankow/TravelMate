using System.ComponentModel.DataAnnotations;

namespace TravelMate.Core.Models.Photo
{
    public class PhotoViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(2097152)]
        public Byte[] Bytes { get; set; } = null!;

        [Required]
        public string FileExtension { get; set; } = null!;
    }
}
