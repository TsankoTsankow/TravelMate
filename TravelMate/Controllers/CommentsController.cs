using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Contracts;

namespace TravelMate.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IPostService postService;

        public CommentsController(
            ICommentService _commentService,
            IPostService _postService)
        {
            this.commentService = _commentService;
            this.postService = _postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewPostComments(int id)
        {
            var post = await postService.GetPostInfoByPostId(id);

            var model = await commentService.GetPostComments(post);

            return View(model);
        }
    }
}
