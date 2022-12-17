using TravelMate.Core.Contracts;
using TravelMate.Core.Models.CountryModels;
using TravelMate.Core.Services;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Tests.UnitTests
{
    [TestFixture]
    public class CountryServiceTests : UnitTestsBase
    {
        private ICountryService countryService;

        [OneTimeSetUp]
        public void SetUp()
            => this.countryService = new CountryService(this.context);

        [Test]
        public void GetAllCountries_ShouldReturnCorrectNumberOfCountries()
        {
            //Arrange:             

            //Act: invoke the GetAllCountries method
            var countries = this.countryService.GetAllCountries().Result;

            //Asseert the countries are not null
            Assert.IsNotNull(countries);

            //Assert: the actual count and the count of the number of countries must be equal
            int actualCount = this.context.Countries.Count();
            Assert.That(actualCount, Is.EqualTo(countries.Count()));

        }

        [Test]
        public void GetAllCountriesNames_ShouldReturnCorrectNames()
        {
            //Arrange: 

            //Act: invoke the GetAllCountriesNames method
            var countries = this.countryService.GetAllCountiresNames().Result.ToArray();

            //Asseert the countries are not null
            Assert.IsNotNull(countries);

            //Assert the expected country name is contained in the countries
            string expectedName = this.Country.Name;
            Assert.Contains(expectedName, countries);
        }

        [Test]
        public void GetCountryById_ShouldReturnCorrectCountry()
        {
            //Arrange: add a valid country name to a variable
            string expectedName = this.Country.Name;

            //Act: invoke the GetCategoryById method
            var country = this.countryService.GetCountryById(Country.Id).Result;

            //Asseert the country is not null
            Assert.IsNotNull(country);

            //Assert the expected country name is the same as the name of the one from the method            
            Assert.That(country.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void AddCountry_ShouldAddNewCountry()
        {
            //Arrange: get the current count of countries
            var countriesInDbBefore = this.context.Countries.Count();

            //Arrange: create a new country variable with needed data
            var newCountry = new EditCountryViewModel()
            {
                Id = 2,
                Name = "New Country",
                Description = "New country description"
            };

            //Act: invoke the Add method
            this.countryService.Add(newCountry);

            //Assert the count of countries has increased by 1
            var countriesInDbAfter = this.context.Countries.Count();
            Assert.That(countriesInDbAfter, Is.EqualTo(countriesInDbBefore + 1));

            //Assert the new coountry is created with the correct data
            var newCreatedCountry = this.context.Countries.Find(newCountry.Id);
            Assert.That(newCreatedCountry.Name, Is.EqualTo(newCountry.Name));
        }

        [Test]
        public void EditCountry_ShouldEditCorrectly()
        {
            //Arrange: get the current count of countries
            var countryForEdit = new Country()
            {
                Id = 3,
                Name = "New Country For Edit",
                Description = "New country for edit description"
            };

            context.Countries.Add(countryForEdit);
            context.SaveChanges();

            //Arrange: create a variable with the changed name
            var editedName = "Edited Country Name";

            var editedCountry = new EditCountryViewModel()
            {
                Id = countryForEdit.Id,
                Name = editedName,
                Description = countryForEdit.Description
            };

            //Act: invoke the Edit method
            this.countryService.Edit(editedCountry);

            //Assert the country is edited with the correct data
            var editedCountryInDb = this.context.Countries.Find(countryForEdit.Id);
            Assert.IsNotNull(editedCountryInDb);
            Assert.That(editedCountryInDb.Name, Is.EqualTo(countryForEdit.Name));
            Assert.That(editedCountryInDb.Description, Is.EqualTo(countryForEdit.Description));

        }

        [Test]
        public void EditCountry_ShouldThroughExceptionWithInvalidCountryId()
        {
            //Arrange: 

            //Arrange: set a variable of a category that is not existing
            var countryId = int.MaxValue;
            var editedCountry = new EditCountryViewModel()
            {
                Id = countryId,
                Name = "Non existent country",
                Description = "Non existent description"
            };

            //Act: 

            //Assert the country is edited with the correct data
            Assert.That(async () => await this.countryService.Edit(editedCountry), Throws.Exception.TypeOf<Exception>());

        }
    }
}
