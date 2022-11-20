using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TravelMate.Core.Models.Photo
{
    public class DisplayPhotoViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string DateAdded { get; set; } = null!;
    }
}
