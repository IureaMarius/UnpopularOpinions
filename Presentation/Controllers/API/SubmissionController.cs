using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels;

namespace Presentation.Controllers.API
{
    public class SubmissionController : ApiController
    {
        private readonly SubmissionService _submissionService;
        public SubmissionController()
        {
            this._submissionService = new SubmissionService();
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult Put([FromBody]EditSubmissionViewModel submissionViewModel)
        {
            try
            {
                SubmissionViewModel updateEntity = this._submissionService.GetSubmissionById(submissionViewModel.SubmissionId);
                updateEntity.Title = submissionViewModel.Title;
                updateEntity.Content = submissionViewModel.Content;
                this._submissionService.UpdateSubmission(updateEntity);
                return Ok("success");
            }catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                this._submissionService.DeleteSubmission(id);
                return Ok("success");// make REST 
            }catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
