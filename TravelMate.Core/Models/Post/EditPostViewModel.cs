using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMate.Core.Models.Post
{
    public class EditPostViewModel : CreatePostViewModel
    {
        public string AuthorId { get; set; } = null!;
    }
}
