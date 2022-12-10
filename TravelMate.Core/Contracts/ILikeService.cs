namespace TravelMate.Core.Contracts
{
    public interface ILikeService 
    {
        Task AddLike(int postId, string userId);
        Task<bool> UserLikedPost(int postId, string userId);
    }
}
