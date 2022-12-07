using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMate.Core.Models.CountryModels;
using TravelMate.Infrastructure.Data;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Core.Models.Profile
{
    public class EditProfileViewModel
    {
        public string UserId { get; set; } = null!;

        [StringLength(UserFirstNameMaxLength, MinimumLength = UserFirstNameMinLength)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [StringLength(UserLastNameMaxLength, MinimumLength = UserLastNameMinLength)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [RegularExpression(@"^(?:0[1-9]|[12]\d|3[01])([\/-])(?:0[1-9]|1[012])\1(?:19|20)\d\d$", ErrorMessage = "Invalid date format")]
        [Display(Name = "Birth date")]
        public string? BirthDate { get; set; }

        [MaxLength(UserInformationMaxLength)]
        [Display(Name = "User Information")]
        public string? Information { get; set; }

        public int? CountryId { get; set; }


        public IEnumerable<CountryUserViewModel> Countries { get; set; } = new List<CountryUserViewModel>();

        public IFormFile? ProfilePicture { get; set; }
    }
}

