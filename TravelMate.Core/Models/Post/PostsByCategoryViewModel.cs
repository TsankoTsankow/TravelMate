﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMate.Core.Models.CategoryModels;

namespace TravelMate.Core.Models.Post
{
    public class PostsByCategoryViewModel
    {
        public string? Category { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
    }
}
