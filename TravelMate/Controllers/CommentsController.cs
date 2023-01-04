using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Comments;
using TravelMate.Extension;

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


        [HttpGet]
        public IActionResult AddComment(int id)
        {
            var model = new AddCommentViewModel();
            model.Id = id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddComment", model);
            }

            string userId = User.Id();
            await commentService.Add(model, userId);

            return RedirectToAction("ViewPostComments", new {@id = model.Id});
        }
    }
}
