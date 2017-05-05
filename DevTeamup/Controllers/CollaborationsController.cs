using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DevTeamup.Dtos;
using DevTeamup.Models;
using Microsoft.AspNet.Identity;

namespace DevTeamup.Controllers
{
    public class CollaborationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CollaborationsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Join(CollaborationDto dto)
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
    }
}
