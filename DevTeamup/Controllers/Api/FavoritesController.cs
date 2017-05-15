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

        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var favorite =
                _context.Favorites.SingleOrDefault(f => f.TeamupId == id && f.FavoringUserId == currentUserId);

            if (favorite == null)
                return NotFound();

            _context.Favorites.Remove(favorite);
            _context.SaveChanges();

            return Ok();
        }

    }
}
