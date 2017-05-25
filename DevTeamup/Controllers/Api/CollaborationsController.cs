using System.Linq;
using System.Web.Http;
using DevTeamup.Dtos;
using DevTeamup.Models;
using Microsoft.AspNet.Identity;

namespace DevTeamup.Controllers.Api
{
    [Authorize]
    public class CollaborationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CollaborationsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Collaborate(CollaborationDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Collaborations.Any(a => a.ContributorId == userId && a.TeamupId == dto.TeamupId))
                return BadRequest("It aready exists");

            var collaboration = new Collaboration
            {
                TeamupId = dto.TeamupId,
                ContributorId = userId
            };

            _context.Collaborations.Add(collaboration);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var collaboration =
                _context.Collaborations.SingleOrDefault(c => c.TeamupId == id && c.ContributorId == currentUserId);

            if (collaboration == null)
                return NotFound();

            _context.Collaborations.Remove(collaboration);
            _context.SaveChanges();

            return Ok();
        }
    }
}
