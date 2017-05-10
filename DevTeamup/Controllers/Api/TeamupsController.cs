using DevTeamup.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace DevTeamup.Controllers.Api
{
    [Authorize]
    public class TeamupsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public TeamupsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var teamup = _context.Teamups
                .Include(t => t.Collaborations.Select(c => c.Contributor))
                .Single(t => t.Id == id && t.OrganizerId == currentUserId);

            if (teamup.IsCanceled)
                return NotFound();

            teamup.Cancel();

           

            _context.SaveChanges();

            return Ok();
        }
    }
}
