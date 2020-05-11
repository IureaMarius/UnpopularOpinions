using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BusinessLogic.Services
{
    public class CommentService
    {
        private readonly SubmissionRepository _submissionRepository;
        private readonly CommentRepository _commentRepository;
        private readonly UserRepository _userRepository;

        public CommentService()
        {
            UnpopularOpinionsDbContext currentContext = new UnpopularOpinionsDbContext();
            this._submissionRepository = new SubmissionRepository(currentContext);
            this._commentRepository = new CommentRepository(currentContext);
            this._userRepository = new UserRepository(currentContext);
        }
        public CommentViewModel GetCommentById(Guid id)
        {
            Comment commentEntity = this._commentRepository.GetCommentById(id);
            CommentViewModel commentModel = new CommentViewModel
            {
                Id = commentEntity.Id,
                Text = commentEntity.Text,
                AuthorId = commentEntity.Author.Id,
                AuthorName = commentEntity.Author.Name,
                Upvotes = commentEntity.Upvotes,
                Downvotes = commentEntity.Downvotes
            };
                 List<CommentViewModel> Comments = _commentRepository.Query()
                .Where(comm => comm.ParentComment.Id == commentEntity.Id)
                .Select(commEntity => new CommentViewModel
                {
                    Id = commEntity.Id,
                    Text = commEntity.Text,
                    Upvotes = commEntity.Upvotes,
                    Downvotes = commEntity.Downvotes,
                    AuthorName = commEntity.Author.Name,
                    AuthorId = commEntity.Author.Id,
                    NrOfReplies = commEntity.NrOfReplies
                }).ToList();

            commentModel.Replies = Comments;
            return commentModel;
        }
        /// <summary>
        /// Returns 100 comments made by a certain user
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <param name="skipNrOfComments">Which 100 comments to choose (paging)</param>
        /// <returns>List of CommentViewModels</returns>
        public List<CommentViewModel> Get100CommentsFromUserById(Guid id, int skipNrOfComments)
        {
            List<CommentViewModel> comments = _commentRepository.Query()
                .Where(comm => comm.Author.Id == id)
                .Skip(skipNrOfComments)
                .Select(commentEntity => new CommentViewModel
                {
                    Id = commentEntity.Id,
                    Text = commentEntity.Text,
                    AuthorId = commentEntity.Author.Id,
                    AuthorName = commentEntity.Author.Name,
                    NrOfReplies = commentEntity.NrOfReplies,
                    Upvotes = commentEntity.Upvotes,
                    Downvotes = commentEntity.Downvotes
                }).ToList();
            return comments;
        }
        /// <summary>
        /// Gets 100 replies from the database (skips skipNrOfComments) 
        /// </summary>
        /// <param name="id">Id of the parent comment</param>
        /// <param name="skipNrOfComments">Nr of comments to skip</param>
        /// <returns>A list of CommentViewModel</returns>
        public List<CommentViewModel> Get100CommentReplies(Guid id, int skipNrOfComments)
        {
            Comment ParentComment = _commentRepository.GetCommentById(id);
            if (ParentComment == null)
            {
                throw new InvalidOperationException($"Parent comment with id {id} could not be found");
            }
            List<CommentViewModel> ChildComments = _commentRepository.Query()
                .Where(comm => comm.ParentComment == ParentComment)
                .OrderBy(comm => comm.Upvotes)
                .Skip(skipNrOfComments)
                .Take(100)
                .Select(commentEntity => new CommentViewModel
                {
                    Id = commentEntity.Id,
                    Text = commentEntity.Text,
                    AuthorId = commentEntity.Author.Id,
                    AuthorName = commentEntity.Author.Name,
                    NrOfReplies = commentEntity.NrOfReplies,
                    Upvotes = commentEntity.Upvotes,
                    Downvotes = commentEntity.Downvotes
                }).ToList();
            return ChildComments;
        }
        /// <summary>
        /// Inserts a new comment to the database
        /// </summary>
        /// <param name="commentViewModel">The comment view model</param>
        /// <returns>The inserted comment Guid</returns>
        public Guid InsertComment(CreateCommentViewModel commentViewModel)
        {
            Comment commentEntity;
            //if the parent is a submission ( top level comment )
            if (commentViewModel.ParentCommentId == Guid.Empty)
            {
                commentEntity = new Comment
                {
                    Id = Guid.NewGuid(),
                    Text = commentViewModel.Text,
                    ParentSubmission = _submissionRepository.GetSubmissionById(commentViewModel.ParentSubmissionId),
                    Author = _userRepository.GetUserById(commentViewModel.AuthorId)
                };
            }
            else // if the parent is another comment ( this comment is a reply )
            {
                commentEntity = new Comment
                {
                    Id = Guid.NewGuid(),
                    Text = commentViewModel.Text,
                    ParentComment = _commentRepository.GetCommentById(commentViewModel.ParentCommentId),
                    Author = _userRepository.GetUserById(commentViewModel.AuthorId)
                };

                Comment AddToNrOfReplies = this._commentRepository.GetCommentById(commentViewModel.ParentCommentId);
                AddToNrOfReplies.NrOfReplies++;
            }

            this._commentRepository.AddComment(commentEntity);
            this._commentRepository.SaveChanges();
            return commentEntity.Id;

        }
        /// <summary>
        /// Deletes comment with given commentId from database
        /// </summary>
        /// <param name="commentId">Id of the comment to be removed</param>
        public void DeleteComment(Guid commentId)
        {
            Comment commentToDelete = this._commentRepository.GetCommentById(commentId);
            if(commentToDelete==null)
            {
                throw new InvalidOperationException($"Comment with id {commentId} not found");
            }
            this._commentRepository.DeleteComment(commentToDelete);
            this._commentRepository.SaveChanges();
        }
        /// <summary>
        /// Updates an existing comment
        /// </summary>
        /// <param name="commentViewModel">The comment view model</param>
        public void UpdateComment(CommentViewModel commentViewModel)
        {
            Comment commentEntityToUpdate = this._commentRepository.GetCommentById(commentViewModel.Id);

            if(commentEntityToUpdate == null)
            {
                throw new InvalidOperationException($"Comment with id {commentViewModel.Id} not found");
            }
            commentEntityToUpdate.Upvotes = commentViewModel.Upvotes;
            commentEntityToUpdate.Downvotes = commentViewModel.Downvotes;
            commentEntityToUpdate.NrOfReplies = commentViewModel.NrOfReplies;
            commentEntityToUpdate.Text = commentViewModel.Text;
            this._commentRepository.SaveChanges();
        }
    }
}
