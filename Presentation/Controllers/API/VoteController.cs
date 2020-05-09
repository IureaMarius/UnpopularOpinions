using BusinessLogic.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels;

namespace Presentation.Controllers.API
{
    public class VoteController : ApiController
    {
        private readonly VoteService _voteService;
        private readonly UserService _userService;
        private readonly SubmissionService _submissionService;
        private readonly CommentService _commentService;
        public VoteController()
        {
            this._voteService = new VoteService();
            this._userService = new UserService();
            this._submissionService = new SubmissionService();
            this._commentService = new CommentService();
        }
        [Authorize]
        [HttpPut]
        public IHttpActionResult CastCommentVote(VoteViewModel voteModel)
        {
            try
            { 
                VoteViewModel voteFromDb = _voteService.GetVoteByCommentAndVoterId(voteModel.SubOrCommId, voteModel.VoterId);
                CommentViewModel commentModel = this._commentService.GetCommentById(voteFromDb.SubOrCommId);
                int voteValue = voteModel.VoteValue - voteFromDb.VoteValue;
                switch(voteValue)
                {
                    case -2:
                        commentModel.Upvotes--;
                        commentModel.Downvotes++;
                        break;
                    case -1:
                        if (voteModel.VoteValue == 0)
                            commentModel.Upvotes--;
                        else commentModel.Downvotes++;
                        break;
                    case 1:
                        if (voteModel.VoteValue == 0)
                            commentModel.Downvotes--;
                        else commentModel.Upvotes++;
                        break;
                    case 2:
                        commentModel.Upvotes++;
                        commentModel.Downvotes--;
                        break;

                }
                this._commentService.UpdateComment(commentModel);
                voteFromDb.VoteValue = voteModel.VoteValue;
                _voteService.UpdateVote(voteFromDb);
                return Ok();

            }catch(InvalidOperationException)
            {
                VoteViewModel voteToInsert = new VoteViewModel
                {
                    Id = Guid.NewGuid(),
                    VoterId = this._userService.GetUserById(Guid.Parse(User.Identity.GetUserId())).Id,
                    VoteType = 0,
                    VoteValue = voteModel.VoteValue,
                    SubOrCommId = voteModel.SubOrCommId
                };
                return Ok();

            }
        }
        [Authorize]
        [HttpPut]
        public IHttpActionResult CastSubmissionVote(VoteViewModel voteModel)
        {
            try
            { 
                VoteViewModel voteFromDb = _voteService.GetVoteBySubmissionAndVoterId(voteModel.SubOrCommId, voteModel.VoterId);
                SubmissionViewModel submissionModel = this._submissionService.GetSubmissionById(voteFromDb.SubOrCommId);
                int voteValue = voteModel.VoteValue - voteFromDb.VoteValue;
                switch(voteValue)
                {
                    case -2:
                        submissionModel.Upvotes--;
                        submissionModel.Downvotes++;
                        break;
                    case -1:
                        if (voteModel.VoteValue == 0)
                            submissionModel.Upvotes--;
                        else submissionModel.Downvotes++;
                        break;
                    case 1:
                        if (voteModel.VoteValue == 0)
                            submissionModel.Downvotes--;
                        else submissionModel.Upvotes++;
                        break;
                    case 2:
                        submissionModel.Upvotes++;
                        submissionModel.Downvotes--;
                        break;
                    default: break;

                }
                this._submissionService.UpdateSubmission(submissionModel);
                voteFromDb.VoteValue = voteModel.VoteValue;
                _voteService.UpdateVote(voteFromDb);
                return Ok();

            }catch(InvalidOperationException)
            {
                VoteViewModel voteToInsert = new VoteViewModel
                {
                    Id = Guid.NewGuid(),
                    VoterId = this._userService.GetUserById(Guid.Parse(User.Identity.GetUserId())).Id,
                    VoteType = 1,
                    VoteValue = voteModel.VoteValue,
                    SubOrCommId = voteModel.SubOrCommId
                };
                return Ok();

            }
        }


    }
}
