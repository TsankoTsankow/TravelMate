using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Profile;
using TravelMate.Core.Services;

namespace TravelMate.Tests.UnitTests
{
    public class ProfileServiceTests : UnitTestsBase
    {
        private IProfileService profileService;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.profileService = new ProfileService(this.context);
        }

        [Test]
        public void DisplayProfileById_ShouldReturnCorrectProfileInfo()
        {
            //Arrange: set the id of the post to a variable and get the post from the context
            string profileId = this.User.Id;
            var profileInDb = this.context.Users.FirstOrDefault(p => p.Id == profileId);

            //Act: envoke the method and get the posts with the given Id
            var profile = this.profileService.DisplayProfileById(profileId).Result;

            //Assert that the data from the method is the same as the one from the query
            Assert.That(profile.FirstName, Is.EqualTo(profileInDb.FirstName));
            Assert.That(profile.LastName, Is.EqualTo(profileInDb.LastName));
            Assert.That(profile.Information, Is.EqualTo(profileInDb.Information));
            Assert.That(profile.CountryId, Is.EqualTo(profileInDb.CountryId));
            Assert.That(profile.ProfilePictureUrl, Is.EqualTo(profileInDb.ProfilePictureUrl));
        }

        [Test]
        public void Edit_ShouldEditTheProfileCorrectly()
        {
            //Arrange: set all the necessary data
            string userId = this.User.Id;
            string url = "https://t4.ftcdn.net/jpg/02/69/82/11/360_F_269821180_UAEWi4xE7JhAqOUvOD1JoBLP0YDvqFvA.jpg";
            string editedFirstName = "Edited First Name";
            string editedLastName = "Edited Last Name";
            string information = "Edited information";

            //Arrange: create the required model
            var model = new EditProfileViewModel()
            {
                UserId = userId,
                FirstName = editedFirstName,
                LastName = editedLastName,
                Information = information
            };

            //Act: envoke the method 
            this.profileService.Edit(userId, model, url);

            //Assert that the information is saved correctly
            var user = this.context.Users.FirstOrDefault(u => u.Id == userId);

            Assert.That(user.FirstName, Is.EqualTo(model.FirstName));
            Assert.That(user.LastName, Is.EqualTo(model.LastName));
            Assert.That(user.Information, Is.EqualTo(model.Information));
            Assert.That(user.CountryId, Is.EqualTo(model.CountryId));
            Assert.That(user.ProfilePictureUrl, Is.EqualTo(url));
        }
    }
}
