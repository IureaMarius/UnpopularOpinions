using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace Presentation.Controllers
{
    public class CommentController : Controller
    {
        private readonly SubmissionService _submissionService;
        private readonly CommentService _commentService;
        
        public CommentController()
        {
            this._submissionService = new SubmissionService();
            this._commentService = new CommentService();
        }
        [HttpGet]
        public ActionResult CommentDetails(Guid id)
        {
            
            CommentViewModel commentModel = this._commentService.GetCommentById(id);
            return View(commentModel);
        }
    }
}