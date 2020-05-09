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

        public UserController()
        {
            this._userService = new UserService();
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
    }
}