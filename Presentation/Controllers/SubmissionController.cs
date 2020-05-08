using System;
using BusinessLogic.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
namespace Presentation.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly SubmissionService _submissionService;
        private readonly CommentService _commentService;

        public SubmissionController()
        {
            this._submissionService = new SubmissionService();
            this._commentService = new CommentService();
        }

        [HttpGet]
        public ActionResult SubmissionList(int skipNrOfSubmissions = 0)
        {
            List<SubmissionListViewModel> submissions = this._submissionService.Get100Submissions(skipNrOfSubmissions);

            return View(submissions);
        }
        [HttpGet]
        public ActionResult SubmissionDetails(Guid id)
        {
            SubmissionViewModel submission = this._submissionService.GetSubmissionById(id);

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
        public ActionResult EditSubmission(EditSubmissionViewModel submission)
        {
            this._submissionService.UpdateSubmission(submission);

            return RedirectToAction("SubmissionList");
        }
        [HttpGet]
        public ActionResult CreateSubmission()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSubmission(CreateSubmissionViewModel submission)
        {
            Guid submissionId = this._submissionService.InsertSubmission(submission);
            return RedirectToAction("SubmissionList");
        }
        [HttpPost]
        public ActionResult DeleteSubmission(Guid id)
        {
            this._submissionService.DeleteSubmission(id);
            return RedirectToAction("SubmissionList");
        }
    }
}