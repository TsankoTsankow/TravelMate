using Microsoft.Extensions.Hosting;
using System.Reflection;
using TravelMate.Infrastructure.Data;
using TravelMate.Infrastructure.Data.Enums;
using TravelMate.Tests.Mocks;

namespace TravelMate.Tests.UnitTests
{
    public class UnitTestsBase
    {
        protected ApplicationDbContext context;

        [OneTimeSetUp]
        public void SetUpBase()
        {
            this.context = DatabaseMock.Instance;
            
            this.SeedDatabase();
        }

        public ApplicationUser User { get; set; }
        public Category Category { get; set; }
        public Country Country { get; set; }
        public Region Region { get; set; }
        public UserFriendship Friendship { get; set; }
        public Post Post { get; set; }
        public Notification Notification { get; set; }
        public Like Like { get; set; }

        private void SeedDatabase()
        {
            this.User = new ApplicationUser()
            {
                Id = "UserTestId2",
                UserName = "Test2",
                NormalizedUserName = "test2",
                Email = "test2@test.com",
                NormalizedEmail = "test2@test.com",
                FirstName = "Test2",
                LastName = "Dve",
                CountryId = 1,
                Information = "Test text two",
                BirthDate = DateTime.Now,
                ProfilePictureUrl = "https://pub-static.fotor.com/assets/projects/pages/d5bdd0513a0740a8a38752dbc32586d0/fotor-03d1a91a0cec4542927f53c87e0599f6.jpg"
            };

            this.context.Users.Add(User);

            this.User = new ApplicationUser()
            {
                Id = "UserTestId3",
                UserName = "Test3",
                NormalizedUserName = "test3",
                Email = "test3@test.com",
                NormalizedEmail = "test3@test.com",
                FirstName = "Test3",
                LastName = "Tri",
                CountryId = 1,
                Information = "Test text three",
                BirthDate = DateTime.Now,
                ProfilePictureUrl = "https://thumbs.dreamstime.com/b/profile-picture-vector-perfect-social-media-other-web-use-125320944.jpg"
            };

            this.context.Users.Add(User);

            this.User = new ApplicationUser()
            {
                Id = "UserTestId1",
                UserName = "Test1",
                NormalizedUserName = "test1",
                Email = "test1@test.com",
                NormalizedEmail = "test1@test.com",
                FirstName = "Test1",
                LastName = "Edno",
                CountryId = 1,
                Information = "Test text one",
                BirthDate = DateTime.Now,
                ProfilePictureUrl = "https://thumbs.dreamstime.com/b/profile-picture-vector-perfect-social-media-other-web-use-125320944.jpg"
            };

            this.context.Users.Add(this.User);


            this.Friendship = new UserFriendship()
            {
                UserId = "UserTestId1",
                UserFriendId = "UserTestId2"
            };

            this.context.UserFriendships.Add(Friendship);

            this.Friendship = new UserFriendship()
            {
                UserId = "UserTestId2",
                UserFriendId = "UserTestId1"
            };

            this.context.UserFriendships.Add(Friendship);

            this.Country = new Country()
            {
                Id = 1,
                Name = "TestCountry",
                Description = "Test country description",
                RegionId = 1
            };

            this.context.Countries.Add(Country);

            this.Region = new Region()
            {
                Id = 1,
                Name = "TestRegion",
                Description = "Test region description",
            };

            this.context.Regions.Add(Region);

            this.Category = new Category()
            {
                Id = 1,
                Name = "TestCategory",
                Description = "Test category description"
            };

            this.context.Categories.Add(Category);

            this.Post = new Post()
            {
                Id = 1,
                CreatedOn = DateTime.Now.AddDays(-3),
                Content = "Test post content one", 
                AuthorId = "UserTestId1",
                PhotoUrl = "https://media.istockphoto.com/id/1221837116/photo/positive-man-celebrating-success.jpg?s=612x612&w=0&k=20&c=UAazDrWbUjSHAYNlthq_kf1IdzsxZo9CCtEYc7zJTAw=",
                CategoryId = 1,
                CountryId = 1, 
                IsDeleted = false
            };

            this.context.Posts.Add(Post);

            this.Post = new Post()
            {
                Id = 2,
                CreatedOn = DateTime.Now.AddDays(-2),
                Content = "Test post content two",
                AuthorId = "UserTestId2",
                PhotoUrl = "https://i.natgeofe.com/k/830b5d15-92db-429f-a80a-cc89b5700af5/mt-everest.jpg?w=636&h=437",
                CategoryId = 1,
                CountryId = 1,
                IsDeleted = false
            };

            this.context.Posts.Add(Post);

            this.Post = new Post()
            {
                Id = 3,
                CreatedOn = DateTime.Now.AddDays(-1),
                Content = "Test post content three",
                AuthorId = "UserTestId3",
                PhotoUrl = "https://assets.traveltriangle.com/blog/wp-content/uploads/2018/09/swiss-alps.jpg",
                CategoryId = 1,
                CountryId = 1,
                IsDeleted = false
            };

            this.context.Posts.Add(Post);

            this.Like = new Like()
            {
                PostId = 1,
                UserId = "UserTestId1"
            };

            this.context.Likes.Add(Like);

            this.Notification = new Notification()
            {
                NotificationType = NotificationType.PostLike,
                Description = "User1 liked your post from yesterday",
                UserId = "UserTestId1",
                User = User,
                SenderId = "UserTestId2",
                IsRead = false
            };

            this.context.Notifications.Add(Notification);

            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDownBase()
            => this.context.Dispose();
    }
}
