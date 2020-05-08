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
    public class CommentController : ApiController
    {
        private readonly CommentService _commentService;

        public CommentController()
        {
            this._commentService = new CommentService();
        }
        [HttpPut]
        public IHttpActionResult Put([FromBody]EditCommentViewModel editCommentViewModel)
        {
            try
            {
                this._commentService.UpdateComment(editCommentViewModel);
                return Ok();
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public void Post([FromBody]CreateCommentViewModel commentModel)
        {
                Guid id = this._commentService.InsertComment(commentModel);
        }
        [HttpDelete]
        public void Delete(Guid id)
        {
            this._commentService.DeleteComment(id);
        }
            
    }
}
