using BusinessLogic.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly CommentService _commentService;
        public UserController()
        {
            this._userService = new UserService();
            this._commentService = new CommentService();
        }
        [HttpGet]
        [Authorize]
        public ActionResult CreateUser()
        {
            try
            {
                if (_userService.GetUserById(Guid.Parse(User.Identity.GetUserId())).Name != null)
                {
                    return RedirectToAction("AccountAlreadyHasName");
                }
            }catch(InvalidOperationException)
            {
                return View();
            }

            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreateUser(CreateUserViewModel userModel)
        {
            this._userService.InsertUser(userModel);
            return RedirectToAction("SubmissionList", "Submission");
        }

       

        [HttpGet]
        public ActionResult AccountAlreadyHasName()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserSubmissions(Guid id, int skipNrOfSubmissions)
        {
            UserSubmissionsViewModel submissions = this._userService.Get100UserSubmissionsById(id, skipNrOfSubmissions);

            if(User.Identity.IsAuthenticated)
                ViewBag.userName = this._userService.GetUserById(Guid.Parse(User.Identity.GetUserId())).Name;
            ViewBag.skipNrOfSubmission = skipNrOfSubmissions;

            return View(submissions);
        }
        [HttpGet]
        public  ActionResult UserComments(Guid id, int skipNrOfComments)
        {
            UserCommentsViewModel comments = this._userService.Get100UserCommentsById(id, skipNrOfComments);
            if(User.Identity.IsAuthenticated)
                ViewBag.userName = this._userService.GetUserById(Guid.Parse(User.Identity.GetUserId())).Name;
            ViewBag.skipNrOfComments= skipNrOfComments;
            return View(comments);

        }
    }
}