﻿using DevTeamup.Models;
using DevTeamup.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace DevTeamup.Controllers
{
    public class TeamupsController : Controller
    {
        private ApplicationDbContext _context;

        public TeamupsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new TeamupFormViewModel
            {
                DevelopmentLanguages = _context.DevelopmentLanguages.ToList(),
                DevelopmentTypes = _context.DevelopmentTypes.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamupFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.DevelopmentLanguages = _context.DevelopmentLanguages.ToList();
                viewModel.DevelopmentTypes = _context.DevelopmentTypes.ToList();

                return View("Create", viewModel);
            }

            var teamup = new Teamup
            {
                OrganizerId = User.Identity.GetUserId(),
                Address = viewModel.Address,
                DateTime = viewModel.GetDateTime(),
                DevelopmentLanguageId = viewModel.DevelopmentLanguage,
                DevelopmentTypeId = viewModel.DevelopmentType,
                Description = viewModel.Description,
            };

            _context.Teamups.Add(teamup);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}