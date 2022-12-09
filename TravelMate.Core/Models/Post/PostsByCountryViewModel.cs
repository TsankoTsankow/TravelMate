using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMate.Core.Models.Post
{
    public class PostsByCountryViewModel
    {
        public string? Country { get; set; }
        public IEnumerable<string> Countries { get; set; }
        public IEnumerable<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
    }
}
