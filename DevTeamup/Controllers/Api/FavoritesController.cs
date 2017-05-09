using System.Linq;
using System.Web.Http;
using DevTeamup.Dtos;
using DevTeamup.Models;
using Microsoft.AspNet.Identity;

namespace DevTeamup.Controllers.Api
{
    public class FavoritesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FavoritesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Favor(FavoriteDto dto)
        {
            var currentUser = User.Identity.GetUserId();

            if (_context.Favorites.Any(f => f.FavoringUserId == currentUser && f.TeamupId == dto.TeamupId))
                return BadRequest("Already favored.");

            var favorite = new Favorite
            {
                FavoringUserId = currentUser,
                TeamupId = dto.TeamupId
            };

            _context.Favorites.Add(favorite);
            _context.SaveChanges();

            return Ok();
        }

    }
}
