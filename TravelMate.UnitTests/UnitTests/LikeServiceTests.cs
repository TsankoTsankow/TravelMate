using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TravelMate.Core.Contracts;
using TravelMate.Core.Services;

namespace TravelMate.Tests.UnitTests
{
    [TestFixture]
    public class LikeServiceTests : UnitTestsBase
    {
        private ILikeService likeService;

        [OneTimeSetUp]
        public void SetUp()
            => this.likeService = new LikeService(this.context);

        [Test]
        public void AddLike_AddsNewLikeWithCorrectData()
        {
            //Arrange: get the number of likes in the DB before 
            int numLikesBefore = this.context.Likes.Count();

            //Arrange: set variables to the ids of the user and the post
            var userId = this.User.Id;
            var postId = this.Post.Id;

            //Act: envoke the AddLike method
            this.likeService.AddLike(postId, userId);

            //Assert that the number of likes has increased with one
            var numLikesAfter = this.context.Likes.Count();
            Assert.That(numLikesAfter, Is.EqualTo(numLikesBefore + 1));

            //Assert that the new like has correct data
            var like =this.context.Likes.Where(l => l.PostId == postId && l.UserId == userId).FirstOrDefault();
            Assert.That(like, Is.Not.Null);
        }

        [Test]
        public void UserLikedPost_ReturnsCorrectBoolean()
        {
            
            //Arrange: set variables to the ids of the user and the post that will get a like from the previous test
            var userId = this.User.Id;
            var postId = this.Post.Id;

            //Arrange: set variables to the ids of a user and post that dont have a like 
            var negativeUserId = "UserTestId2";
            var negativePostId = 2;

            //Act: envoke the UserLikedPost method for both cases
            bool positive = this.likeService.UserLikedPost(postId, userId).Result;
            bool negative = this.likeService.UserLikedPost(negativePostId, negativeUserId).Result;

            //Assert that the positive test is positive
            //This test should be negative if it is run by itself!!!
            Assert.That(positive, Is.True);

            //Assert that the negative test is negative
            Assert.That(negative, Is.False);
        }

    }
}
