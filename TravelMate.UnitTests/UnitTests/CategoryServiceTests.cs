using TravelMate.Core.Contracts;
using TravelMate.Core.Models.CategoryModels;
using TravelMate.Core.Services;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Tests.UnitTests
{
    [TestFixture]
    public class CategoryServiceTests : UnitTestsBase
    {
        private ICategoryService categoryService;

        [OneTimeSetUp]
        public void SetUp()
            => this.categoryService = new CategoryService(this.context);

        [Test]
        public void GetAllCategories_ShouldReturnCorrectNumberOfCategories()
        {
            //Arrange:             

            //Act: invoke the GetAllCategories method
            var categories = this.categoryService.GetAllCategories().Result;

            //Asseert the categories are not null
            Assert.IsNotNull(categories);

            //Assert: the actual count and the count of the number of categories must be equal
            int actualCount = this.context.Categories.Count();
            Assert.That(actualCount, Is.EqualTo(categories.Count()));

        }

        [Test]
        public void GetAllCategoriesNames_ShouldReturnCorrectNames()
        {
            //Arrange: 
            
            //Act: invoke the GetAllCategoriesNames method
            var categories = this.categoryService.GetAllCategoriesNames().Result.ToArray();

            //Asseert the categories are not null
            Assert.IsNotNull(categories);

            //Assert the expected category name is contained in the categories
            string expectedName = this.Category.Name;
            Assert.Contains(expectedName, categories);
        }

        [Test]
        public void GetCategoryById_ShouldReturnCorrectCategory()
        {
            //Arrange: add a valid category name to a variable
            string expectedName = this.Category.Name;

            //Act: invoke the GetCategoryById method
            var category = this.categoryService.GetCategoryById(Category.Id).Result;

            //Asseert the category is not null
            Assert.IsNotNull(category);

            //Assert the expected category name is the same as the name of the one from the method            
            Assert.That(category.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void AddCategory_ShouldAddNewCategory()
        {
            //Arrange: get the current count of categories
            var categoriesInDbBefore = this.context.Categories.Count();

            //Arrange: create a new category variable with needed data
            var newCategory = new EditCategoryViewModel()
            {
                Id = 2,
                Name = "New Category",
                Description = "New category description"
            };

            //Act: invoke the Add method
            this.categoryService.Add(newCategory);

            //Assert the count of categories has increased by 1
            var categoriesInDbAfter = this.context.Categories.Count();
            Assert.That(categoriesInDbAfter, Is.EqualTo(categoriesInDbBefore + 1));

            //Assert the new category is created with the correct data
            var newCreatedCategory = this.context.Categories.Find(newCategory.Id);
            Assert.That(newCreatedCategory.Name, Is.EqualTo(newCategory.Name));
        }

        [Test]
        public void EditCategory_ShouldEditCorrectly()
        {
            //Arrange: get the current count of categories
            var categoryForEdit = new Category()
            {
                Id = 3,
                Name = "New Category For Edit",
                Description = "New category for edit description"
            };

            context.Categories.Add(categoryForEdit);
            context.SaveChanges();

            //Arrange: create a variable with the changed name
            var editedName = "Edited Category Name";

            var editedCategory = new EditCategoryViewModel()
            {
                Id = categoryForEdit.Id, 
                Name = editedName, 
                Description = categoryForEdit.Description
            };

            //Act: invoke the Edit method
            this.categoryService.Edit(editedCategory);

            //Assert the category is edited with the correct data
            var editedCategoryInDb = this.context.Categories.Find(categoryForEdit.Id);
            Assert.IsNotNull(editedCategoryInDb);
            Assert.That(editedCategoryInDb.Name, Is.EqualTo(categoryForEdit.Name));
            Assert.That(editedCategoryInDb.Description, Is.EqualTo(categoryForEdit.Description));

        }

        [Test]
        public void EditCategory_ShouldThroughExceptionWithInvalidCategoryId()
        {
            //Arrange: 

            //Arrange: set a variable of a category that is not existing
            var categoryId = int.MaxValue;
            var editedCategory = new EditCategoryViewModel()
            {
                Id = categoryId,
                Name = "Non existent category",
                Description = "Non existent description"
            };

            //Act: 

            //Assert the category is edited with the correct data
            Assert.That(async() => await this.categoryService.Edit(editedCategory), Throws.Exception.TypeOf<Exception>());

        }
    }
}
