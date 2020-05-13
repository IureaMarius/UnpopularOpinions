using System;
using BusinessLogic.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Ajax.Utilities;


namespace Presentation.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly SubmissionService _submissionService;
        private readonly CommentService _commentService;
        private readonly UserService _userService;

        public SubmissionController()
        {
            this._submissionService = new SubmissionService();
            this._commentService = new CommentService();
            this._userService = new UserService();
        }

        [HttpGet]
        public ActionResult SubmissionList(int skipNrOfSubmissions = 0)
        {
            if (!isLoggedRegisteredInDb())
                return RedirectToAction("CreateUser", "User");
            List<SubmissionListViewModel> submissions = this._submissionService.Get100Submissions(skipNrOfSubmissions);
            if(User.Identity.IsAuthenticated)
                ViewBag.userName = this._userService.GetUserById(Guid.Parse(User.Identity.GetUserId())).Name;
            ViewBag.skipNrOfSubmission = skipNrOfSubmissions;
            return View(submissions);
        }
        [HttpGet]
        public ActionResult SubmissionDetails(Guid id)
        {
            
            if (!isLoggedRegisteredInDb())
                return RedirectToAction("CreateUser", "User");
            SubmissionViewModel submission = this._submissionService.GetSubmissionById(id);
            ViewBag.userName = this._userService.GetUserById(submission.AuthorId).Name;
            return View(submission);
        }
        [HttpGet]
        public ActionResult EditSubmission(Guid id)
        {
            SubmissionViewModel submission = this._submissionService.GetSubmissionById(id);

            EditSubmissionViewModel editViewModel = new EditSubmissionViewModel
            {
                Title = submission.Title,
                Content = submission.Content,
                AuthorId = submission.AuthorId,
                SubmissionId = submission.Id

            };

            return View(editViewModel);
        }
        [HttpPost]
        public ActionResult EditSubmission(SubmissionViewModel submission)
        {
            if(Guid.Parse(User.Identity.GetUserId())==submission.AuthorId)
                this._submissionService.UpdateSubmission(submission);

            return RedirectToAction("SubmissionList");
        }
        [HttpGet]
        [Authorize]
        public ActionResult CreateSubmission()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSubmission(CreateSubmissionViewModel submission)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            Guid submissionId = this._submissionService.InsertSubmission(submission);
            return RedirectToAction("SubmissionList");
        }
        [HttpPost]
        public ActionResult DeleteSubmission(Guid id)
        {
            this._submissionService.DeleteSubmission(id);
            return RedirectToAction("SubmissionList");
        }
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        //Helper function used to register user's account after external login
        public bool isLoggedRegisteredInDb()
        {
            if(User.Identity.GetUserId()==null)
            {
                return true; //isn't logged in
            }
            try
            {
                CreateUserViewModel userModel = _userService.GetUserById(Guid.Parse(User.Identity.GetUserId()));
            }catch(InvalidOperationException)
            {
                return false;// logged with google but not registered
            }
            return true;// is logged and registered
        }
    }
}