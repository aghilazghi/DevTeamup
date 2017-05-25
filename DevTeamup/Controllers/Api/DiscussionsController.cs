using AutoMapper;
using DevTeamup.Dtos;
using DevTeamup.Models;
using DevTeamup.Models.Extensions;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace DevTeamup.Controllers.Api
{
    //[Authorize]
    public class DiscussionsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public DiscussionsController()
        {
            _context = new ApplicationDbContext();
        }

        
        public IHttpActionResult Get(int id)
        {
            var discussions = _context.Discussions
                .Where(d => d.TeamupId == id)
                .Include(d => d.Replies)
                .OrderBy(d => d.CreatedOn)
                .ToList();

            return Ok(discussions.Select(Mapper.Map<Discussion, DiscussionDto>));
        }

        [HttpPost]
        public IHttpActionResult PostDiscussion(DiscussionDto dto)
        {
            var currentUser = User.Identity.GetUserFirstname();

            var discussion = new Discussion
            {
                PostedBy = currentUser,
                Body = dto.Body,
                TeamupId = dto.TeamupId
            };

            _context.Discussions.Add(discussion);
            _context.SaveChanges();

            return Ok(discussion);
        }
    }
}
