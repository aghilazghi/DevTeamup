using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DevTeamup.Models;
using Microsoft.AspNet.Identity;

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
            var teamup = _context.Teamups.Single(t => t.Id == id && t.OrganizerId == currentUserId);

            if (teamup.IsCancelled)
                return NotFound();

            teamup.IsCancelled = true;

            _context.SaveChanges();

            return Ok();
        }
    }
}
