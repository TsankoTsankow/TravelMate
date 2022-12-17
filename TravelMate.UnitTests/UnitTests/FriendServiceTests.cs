using TravelMate.Core.Contracts;
using TravelMate.Core.Services;

namespace TravelMate.Tests.UnitTests
{
    [TestFixture]
    public class FriendServiceTests : UnitTestsBase
    {
        private IFriendService friendService;

        [OneTimeSetUp]
        public void SetUp()
            => this.friendService = new FriendService(this.context);

        [Test]
        public void UsersAreFriends_ShouldReturnCorrectBoolInfo()
        {
            //Arrange: set variables to the ids of the three seeded users
            string userId = this.User.Id;
            string friendId = "UserTestId2";
            string notFriend = "UserTestId3";

            //Act: envoke the method for a couple of users that are friends and a couple that are not
            bool areFriends = this.friendService.UsersAreFriends(userId, friendId).Result;
            bool areNotFriends = this.friendService.UsersAreFriends(userId, notFriend).Result;

            //Assert should return true in the first case and false in the second
            Assert.That(areFriends, Is.True);
            Assert.That(areNotFriends, Is.False);
        }

        [Test]
        public void GetAllFriends_ShouldReturnCorrectNumberOfFriends()
        {
            //Arrange: set variables to the id of one of the users who has friends
            string userId = this.User.Id;
            string noFriendId = "UserTestId3";

            //Arrange: set variables for the numbers of friends of both users
            int numOfFriends = context.UserFriendships.Where(uf => uf.UserId == userId).Count();
            int zeroFriends = context.UserFriendships.Where(uf => uf.UserId == noFriendId).Count();

            //Act: invoke the GetAllFriends method for a user with one friend and a user with no friends
            var result = this.friendService.GetAllFriends(userId).Result.ToList().Count();
            var zeroResult = this.friendService.GetAllFriends(noFriendId).Result.ToList().Count();

            //Assert the number of friends from the method are the same as in the DB
            Assert.That(result, Is.EqualTo(numOfFriends));
            Assert.That(zeroResult, Is.EqualTo(zeroFriends));
        }

        [Test]
        public void AddFriend_ShouldIncreaseTheNumberOfFriendsOfBothUsers()
        {
            //Arrange: set variables to the ids of two users that are not friends
            string userId = this.User.Id;
            string friendId = "UserTestId3";

            //Arrange: get the initial number of friends
            int initNumOfFriendsOfUser = context.UserFriendships.Where(uf => uf.UserId == userId).Count();
            int initNumOfFriendsOfFriend = context.UserFriendships.Where(uf => uf.UserId == friendId).Count();

            //Act: invoke the method to make both users friends
            this.friendService.AddFriend(userId, friendId);

            //Assert should return true
            int afterNumOfFriendsOfUser = context.UserFriendships.Where(uf => uf.UserId == userId).Count();
            int afterNumOfFriendsOfFriend = context.UserFriendships.Where(uf => uf.UserId == friendId).Count();

            Assert.That(afterNumOfFriendsOfUser, Is.EqualTo(initNumOfFriendsOfUser + 1));
            Assert.That(afterNumOfFriendsOfFriend, Is.EqualTo(initNumOfFriendsOfFriend + 1));
        }

        [Test]
        public void AddFriend_ShouldThrowExceptionWithWrongId()
        {
            //Arrange: set variables to inexistent ids of two users 
            string userId = Guid.NewGuid().ToString();
            string friendId = Guid.NewGuid().ToString();

            //Act: 

            //Assert the method with wrong IDs should throw an exception
            Assert.That(async () => await this.friendService.AddFriend(userId, friendId), Throws.Exception.TypeOf<ArgumentException>());
        }
    }
}
