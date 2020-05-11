using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Protocols;
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
        [Authorize]
        [HttpPut]
        public IHttpActionResult Put([FromBody]EditCommentViewModel commentViewModel)
        {
            try
            {
                CommentViewModel updateEntity = this._commentService.GetCommentById(commentViewModel.CommentId);

                updateEntity.Text = commentViewModel.Text;

                this._commentService.UpdateComment(updateEntity);
                return Ok("success");
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
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post([FromBody]CreateCommentViewModel commentModel)
        {
            try { 
                Guid id = this._commentService.InsertComment(commentModel);
                return Ok(id);
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
                this._commentService.DeleteComment(id);
                return Ok("success");
            } catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
            
    }
}
