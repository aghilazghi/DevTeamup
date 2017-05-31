using System;
using AutoMapper;
using DevTeamup.Dtos;
using DevTeamup.Models;
using DevTeamup.Models.Extensions;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace DevTeamup.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/discussions")]
    public class DiscussionsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public DiscussionsController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var discussions = _context.Discussions
                .Where(d => d.TeamupId == id)
                .Include(d => d.Replies)
                .OrderBy(d => d.CreatedOn)
                .ToList();

            return Ok(discussions.Select(Mapper.Map<Discussion, DiscussionDto>));
        }

        [HttpGet]
        [Route("getImage")]
        public HttpResponseMessage GetImage(string id)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            var image = _context.Users.FirstOrDefault(u => u.Id == id && u.UserImage != null);

            if (image == null)
                GetDefaultImage(response);
            else
                GetImageFromDb(response, image);

            return response;          
        }

        private static void GetDefaultImage(HttpResponseMessage response)
        {
            var path = "~/images/default.png";
            path = HostingEnvironment.MapPath(path);
            var ext = Path.GetExtension(path);
            var contents = File.ReadAllBytes(path);
            var ms = new MemoryStream(contents);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType =
                new MediaTypeHeaderValue("image/" + ext);
        }

        private static void GetImageFromDb(HttpResponseMessage response, ApplicationUser image)
        {
            var bytes = image.UserImage;
            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment") { FileName = image.FirstName };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        }

        [HttpPost]
        public IHttpActionResult PostDiscussion(DiscussionDto dto)
        {
            var currentUser = User.Identity.GetUserFirstname();
            var currentUserId = User.Identity.GetUserId();

            var discussion = new Discussion
            {
                PostedByName = currentUser,
                PostedById = currentUserId,
                Body = dto.Body,
                TeamupId = dto.TeamupId
            };

            _context.Discussions.Add(discussion);
            _context.SaveChanges();

            return Ok(discussion);
        }
    }
}
