using DevTeamup.Dtos;
using DevTeamup.Models;
using DevTeamup.Models.Extensions;
using System.Web.Http;

namespace DevTeamup.Controllers.Api
{
    [Authorize]
    public class RepliesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public RepliesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult PostReply(ReplyDto dto)
        {

            var currentUser = User.Identity.GetUserFirstname();

            var reply = new Reply
            {
                RepliedBy = currentUser,
                Body = dto.Body,
                DiscussionId = dto.DiscussionId
            };

            _context.Replies.Add(reply);
            _context.SaveChanges();

            return Ok(reply);
        }
    }
}
