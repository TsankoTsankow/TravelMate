using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Post;
using TravelMate.Core.Services;

namespace TravelMate.Tests.UnitTests
{
    public class PostServiceTests : UnitTestsBase
    {
        private IPostService postService;
        private IFriendService friendService;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.friendService = new FriendService(this.context);
            this.postService = new PostService(this.context, this.friendService);
        }


        [Test]
        public void Edit_ShouldEditPostCorrectly()
        {
            //Arrange: set the necessary data
            string url = "https://t4.ftcdn.net/jpg/02/69/82/11/360_F_269821180_UAEWi4xE7JhAqOUvOD1JoBLP0YDvqFvA.jpg";
            var model = new EditPostViewModel()
            {
                Id = 2,
                Content = "New edited test post content",
                CategoryId = 1,
                CountryId = 1,
                AuthorId = "UserTestId2"
            };

            //Act: envoke the method 
            this.postService.Edit(model, model.Id, url);

            //Assert that all data is saved correctly
            var post = this.context.Posts.FirstOrDefault(p => p.Id == model.Id);
            Assert.That(post.Content, Is.SameAs(model.Content));
            Assert.That(post.PhotoUrl, Is.SameAs(url));
            Assert.That(post.CategoryId, Is.EqualTo(model.CategoryId));
            Assert.That(post.CountryId, Is.EqualTo(model.CountryId));
            Assert.That(post.AuthorId, Is.EqualTo(model.AuthorId));
        }

        [Test]
        public void Edit_ShouldThrowErrorWithWrongPostId()
        {
            //Arrange: set the necessary data with wrong postId
            int postId = int.MaxValue;
            string url = "https://t4.ftcdn.net/jpg/02/69/82/11/360_F_269821180_UAEWi4xE7JhAqOUvOD1JoBLP0YDvqFvA.jpg";
            var model = new EditPostViewModel()
            {
                Id = 2,
                Content = "New edited test post content",
                CategoryId = 1,
                CountryId = 1,
                AuthorId = "UserTestId2"
            };

            //Act: 

            //Assert that the method throws an exception  with invalid Id
            Assert.That(async () => await this.postService.Edit(model, postId, url), Throws.Exception.TypeOf<ArgumentException>());

        }

        [Test]
        public void GetAllPostsQuery_ShouldReturnCorrectNumberOfResults()
        {
            //Arrange: 
            string category = "TestCategory";
            string country = "TestCountry";
            int totalPosts = this.context.Posts
                .Where(p => p.IsDeleted == false)
                .Count();
            int totalPostsWithFilter = this.context.Posts
                .Where(p => p.PostCategory.Name == category && p.Country.Name == country && p.IsDeleted == false)
                .ToList().Count();

            //Act: 
            var resultWithoutFilter = this.postService.GetAllPostsQuery().Result;
            var resultWithFilter = this.postService.GetAllPostsQuery(category, country).Result;

            //Assert that the data from the method is the same as form the query
            int postsWithoutFilter = resultWithoutFilter.Posts.Count();
            int postsWithFilter = resultWithFilter.Posts.Count();
            Assert.That(postsWithoutFilter, Is.EqualTo(totalPosts));
            Assert.That(postsWithFilter, Is.EqualTo(totalPostsWithFilter));
        }

        [Test]
        public void GetAllPostsByCategoryId_ShouldReturnCorrectNumberOfPosts()
        {
            //Arrange get the number of posts with the same criteria from the DB
            int categoryId = 1;
            int postsInDb = this.context.Posts
                .Where(p => p.IsDeleted == false)
                .Count(p => p.CategoryId == categoryId);

            //Act: envoke the method and get the number of posts it returns with the same criteria
            int postsByMethod = this.postService.GetAllPostsByCategoryId(categoryId).Result.ToList().Count();

            //Assert that both results are the same
            Assert.That(postsByMethod, Is.EqualTo(postsInDb));
        }

        [Test]
        public void CreatePost_ShouldCreatePostCorrectly()
        {
            //Arrange: set the necessary data
            string userId = this.User.Id;
            string url = "https://t4.ftcdn.net/jpg/02/69/82/11/360_F_269821180_UAEWi4xE7JhAqOUvOD1JoBLP0YDvqFvA.jpg";
            var model = new CreatePostViewModel()
            {
                Id = 4,
                Content = "New test post content",
                CategoryId = 1,
                CountryId = 1,
            };

            //Arrange: get the total number of posts
            int totalNumberOfPostsBefore = this.context.Posts.Count();

            //Arrange: get the total number of posts of the user
            int numberOfUserPostsBefore = this.context.Posts.Where(p => p.AuthorId == userId).Count();

            //Act: envoke the method 
            this.postService.CreatePost(model, userId, url);

            //Assert that the total number of posts after creating a new one has increased with one
            int totalNumberOfPostsafter = this.context.Posts.Count();
            Assert.That(totalNumberOfPostsafter, Is.EqualTo(totalNumberOfPostsBefore + 1));

            //Assert that the users number of posts after creating a new one has increased with one
            int numberOfUserPostsAfter = this.context.Posts.Where(p => p.AuthorId == userId).Count();
            Assert.That(numberOfUserPostsAfter, Is.EqualTo(numberOfUserPostsBefore + 1));

            //Assert that all data is saved correctly
            var post = this.context.Posts.FirstOrDefault(p => p.Id == model.Id);
            Assert.That(post.Content, Is.SameAs(model.Content));
            Assert.That(post.PhotoUrl, Is.SameAs(url));
            Assert.That(post.CategoryId, Is.EqualTo(model.CategoryId));
            Assert.That(post.CountryId, Is.EqualTo(model.CountryId));
            Assert.That(post.AuthorId, Is.EqualTo(userId));
        }

        [Test]
        public void CreatePost_ShouldThrowErrorWithWrongUserId()
        {
            //Arrange: set the necessary data with wrong userId, everything else is correct
            string userId = Guid.NewGuid().ToString();
            string url = "https://t4.ftcdn.net/jpg/02/69/82/11/360_F_269821180_UAEWi4xE7JhAqOUvOD1JoBLP0YDvqFvA.jpg";
            var model = new CreatePostViewModel()
            {
                Id = 4,
                Content = "New test post content",
                CategoryId = 1,
                CountryId = 1,
            };

            //Act: envoke the method 
            this.postService.CreatePost(model, userId, url);

            //Assert that the method throws an exception  with invalid Id
            Assert.That(async () => await this.postService.CreatePost(model, userId, url), Throws.Exception.TypeOf<ArgumentException>());

        }

        [Test]
        public void Delete_ShouldMarkPostAsDeleted()
        {
            //Arrange: set the necessary data
            int postId = 3;

            //Act: envoke the delete method
            this.postService.Delete(postId);

            //Assert that the post is marked as deleted
            var post = this.context.Posts.FirstOrDefault(p => p.Id == postId);
            Assert.That(post.IsDeleted, Is.True);
        }

        [Test]
        public void Delete_ShouldThrowErrorWithWrongPostId()
        {
            //Arrange: set the necessary data with wrong postId
            int postId = int.MaxValue;

            //Act: 

            //Assert that the method throws an exception  with invalid Id
            Assert.That(async () => await this.postService.Delete(postId), Throws.Exception.TypeOf<ArgumentException>());

        }

        [Test]
        public void GetAllPostsByUserId_ShouldReturnCorrectNumberOfPosts()
        {
            //Arrange: get the number of posts with the same criteria from the DB
            string userId = this.User.Id;
            int postsInDb = this.context.Posts
                .Where(p => p.IsDeleted == false)
                .Count(p => p.AuthorId == userId);

            //Act: envoke the method and get the number of posts it returns with the same criteria
            int postsByMethod = this.postService.GetAllPostsByUserId(userId).Result.ToList().Count();

            //Assert that both results are the same
            Assert.That(postsByMethod, Is.EqualTo(postsInDb));
        }

        [Test]
        public void GetAllPostsOfUserFriends_ShouldReturnCorrectNumberOfPosts()
        {
            //Arrange: get the number of posts with the same criteria from the DB
            string userId = this.User.Id;
            var postsOfUserFriends = 1;

            //Act: envoke the method and get the number of posts it returns with the same criteria
            int postsByMethod = this.postService.GetAllPostsOfUserFriends(userId).Result.ToList().Count();

            //Assert that both results are the same
            Assert.That(postsByMethod, Is.EqualTo(postsOfUserFriends));
        }

        [Test]
        public void GetPostById_ShouldReturnCorrectPost()
        {
            //Arrange: set the id of the post to a variable and get the post from the context
            int postId = 1;
            var postInDb = this.context.Posts.FirstOrDefault(p => p.Id == postId);

            //Act: envoke the method and get the posts with the given Id
            var post = this.postService.GetPostById(postId).Result;

            //Assert that both results are the same
            Assert.That(post.Content, Is.EqualTo(postInDb.Content));
            Assert.That(post.AuthorId, Is.EqualTo(postInDb.AuthorId));
            Assert.That(post.CategoryId, Is.EqualTo(postInDb.CategoryId));
        }

        [Test]
        public void GetPostInfoByPostId_ShouldReturnCorrectPostInfo()
        {
            //Arrange: set the id of the post to a variable and get the post from the context
            int postId = 1;
            var postInDb = this.context.Posts.FirstOrDefault(p => p.Id == postId);

            //Act: envoke the method and get the posts with the given Id
            var post = this.postService.GetPostInfoByPostId(postId).Result;

            //Assert that both results are the same
            Assert.That(post.Content, Is.EqualTo(postInDb.Content));
            Assert.That(post.AuthorId, Is.EqualTo(postInDb.AuthorId));
            Assert.That(post.PhotoUrl, Is.EqualTo(postInDb.PhotoUrl));
        }

    }
}


