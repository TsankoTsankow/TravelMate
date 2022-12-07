using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Models.Profile
{
    public class PersonalProfileViewModel
    {
        public string UserId { get; set; } = null!;

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Birth date")]
        public string? BirthDate { get; set; }

        [Display(Name = "First Name")]
        public string? Information { get; set; }

        public int? CountryId { get; set; }

        [Display(Name = "Location")]
        public Country? Country { get; set; }

        public string? ProfilePictureUrl { get; set; }
    }
}
