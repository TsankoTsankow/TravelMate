using TravelMate.Core.Contracts;
using TravelMate.Core.Services;

namespace TravelMate.Tests.UnitTests
{
    public class NotificationServiceTests : UnitTestsBase
    {   
        private INotificationService notificationService;

        [OneTimeSetUp]
        public void SetUp()
            => this.notificationService = new NotificationService(this.context);

        [Test]
        public void GetNotificationsByUserId_ShouldReturnCorrectNumberOfNotifications()
        {
            //Arrange: set a variable to the userId
            string userId = this.User.Id;

            //Act: envoke the method and get the number of notifications
            int notificationCount = this.notificationService.GetNotificationsByUserId(userId).Result.ToList().Count();

            //Assert that the number of notifications from the method are the same as in the DB
            var numNotifications = this.context.Notifications.Where(x => x.UserId == userId).Count();
            Assert.That(notificationCount, Is.EqualTo(numNotifications));

        }

        [Test]
        public void SendFriendRequest_ShouldSendRequestWithCorrectData()
        {
            //Arrange: set a variable to the userId
            string userId = this.User.Id;
            string wantToBefriendId = "UserTestId3";

            //Arrange: get the number of notifications of the user before the request
            int userNotificationCountBefore = this.User.Notifications.Count;

            //Arrange: get the total number of notifications before the request
            var numNotificationsBefore = this.context.Notifications.Count();

            //Act: envoke the method 
            this.notificationService.SendFriendRequest(wantToBefriendId, userId);

            //Assert that the total number of notifications has increased with one
            var numNotificationsAfter = this.context.Notifications.Count();
            Assert.That(numNotificationsAfter, Is.EqualTo(numNotificationsBefore + 1));

            //Assert that the notifications of the user has increased with one
            int userNotificationCountAfter = this.User.Notifications.Count;
            Assert.That(userNotificationCountAfter, Is.EqualTo(userNotificationCountBefore + 1));
        }

        [Test]
        public void SendFriendRequest_ThrowsExceptionWithInvalidUserData()
        {
            //Arrange: set variables to inexistent user Ids
            string userId = Guid.NewGuid().ToString();
            string wantToBefriendId = Guid.NewGuid().ToString();

            //Act:

            //Assert that the method throws an exception
            Assert.That(async () => await this.notificationService.SendFriendRequest(wantToBefriendId, userId), Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]

        public void SendLikeNotification_ShouldSendNotificationWithCorrectData() 
        {
            //Arrange: set a variable to the userId
            string senderId = this.User.Id;
            int postId = 3;

            //Arrange: get the number of notifications of the user before the request
            var post = this.context.Posts.FirstOrDefault(p => p.Id == postId);
            var recepient = this.context.Users.FirstOrDefault(u => u.Id == post.AuthorId);
            int userNotificationCountBefore = recepient.Notifications.Count();

            //Arrange: get the total number of notifications before the request
            var numNotificationsBefore = this.context.Notifications.Count();

            //Act: envoke the method 
            this.notificationService.SendLikeNotification(postId, senderId);

            //Assert that the total number of notifications has increased with one
            var numNotificationsAfter = this.context.Notifications.Count();
            Assert.That(numNotificationsAfter, Is.EqualTo(numNotificationsBefore + 1));

            //Assert that the notifications of the user has increased with one
            int userNotificationCountAfter = recepient.Notifications.Count;
            Assert.That(userNotificationCountAfter, Is.EqualTo(userNotificationCountBefore + 1));
        }

        [Test]
        public void SendLikeNotification_ThrowsExceptionWithInvalidPostData()
        {
            //Arrange: set variables to inexistent post id and existing user id
            string userId = this.User.Id;
            int postId = int.MaxValue;

            //Act:

            //Assert that the method throws an exception
            Assert.That(async () => await this.notificationService.SendLikeNotification(postId, userId), Throws.Exception.TypeOf<ArgumentException>());
        }

        [Test]
        public void SendLikeNotification_ThrowsExceptionWithInvalidUserData()
        {
            //Arrange: set variables to inexistent user id and existing post id
            string userId = Guid.NewGuid().ToString();
            int postId = 3;

            //Act:

            //Assert that the method throws an exception
            Assert.That(async () => await this.notificationService.SendLikeNotification(postId, userId), Throws.Exception.TypeOf<ArgumentException>());
        }
    }
}
