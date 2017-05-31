using DevTeamup.Dtos;
using DevTeamup.Models;
using DevTeamup.Models.Extensions;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;

namespace DevTeamup.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/replies")]
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
            var currentUserId = User.Identity.GetUserId();

            var reply = new Reply
            {
                RepliedByName = currentUser,
                RepliedById = currentUserId,
                Body = dto.Body,
                DiscussionId = dto.DiscussionId
            };

            _context.Replies.Add(reply);
            _context.SaveChanges();

            return Ok(reply);
        }
    }
}
