﻿using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //dependancy injection
        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ViewResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(userId);

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _unitOfWork.Attendance.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genre.GetGenres(),
                Heading = "Add a Gig"
            };

            return View("GigForm", viewModel);
        }

        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending =
                    _unitOfWork.Attendance.GetAttendance(gig.Id, userId) != null;

                viewModel.IsFollowing =
                    _unitOfWork.Following.GetFollowing(userId, gig.ArtistId) != null;
            }

            return View("Details", viewModel);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            //query string so that you can book mark the page
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = new ApplicationDbContext().Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genre.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = new ApplicationDbContext().Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

    }
}