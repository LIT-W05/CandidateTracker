using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CandidateTracker.Data;
using CandidateTracker.Web.Models;

namespace CandidateTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Page = PageType.Home;
            return View();
        }

        public ActionResult AddCandidate()
        {
            ViewBag.Page = PageType.Add;
            return View();
        }

        [HttpPost]
        public ActionResult AddCandidate(Candidate candidate)
        {
            var manager = new CandidateRepository(Properties.Settings.Default.ConStr);
            manager.AddCandidate(candidate);
            return Redirect("/home/pending");
        }

        public ActionResult Pending()
        {
            ViewBag.Page = PageType.Pending;
            var manager = new CandidateRepository(Properties.Settings.Default.ConStr);
            return View(new CandidatesViewModel { Candidates = manager.GetCandidates(Status.Pending) });
        }

        public ActionResult Details(int id)
        {
            ViewBag.Page = PageType.Pending;
            var manager = new CandidateRepository(Properties.Settings.Default.ConStr);
            return View(new CandidateViewModel { Candidate = manager.GetCandidate(id) });
        }

        [HttpPost]
        public void UpdateStatus(int id, Status status)
        {
            var manager = new CandidateRepository(Properties.Settings.Default.ConStr);
            manager.UpdateStatus(id, status);
        }

        public ActionResult GetCounts()
        {
            var manager = new CandidateRepository(Properties.Settings.Default.ConStr);
            return Json(manager.GetCounts(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Confirmed()
        {
            ViewBag.Page = PageType.Confirmed;
            var manager = new CandidateRepository(Properties.Settings.Default.ConStr);
            return View("Completed", new CandidatesViewModel
            {
                Candidates = manager.GetCandidates(Status.Confirmed),
                Type = "Confirmed"
            });
        }

        public ActionResult Refused()
        {
            ViewBag.Page = PageType.Refused;
            var manager = new CandidateRepository(Properties.Settings.Default.ConStr);
            return View("Completed", new CandidatesViewModel
            {
                Candidates = manager.GetCandidates(Status.Refused),
                Type = "Refused"
            });
        }

    }
}