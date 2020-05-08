using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels;

namespace BusinessLogic.Services
{
    public class SubmissionService
    {
        private readonly SubmissionRepository _submissionRepository;
        private readonly UserRepository _userRepository;
        private readonly CommentRepository _commentRepository;
        public SubmissionService()
        {
            UnpopularOpinionsDbContext currentContext = new UnpopularOpinionsDbContext();
            this._submissionRepository = new SubmissionRepository(currentContext);
            this._userRepository = new UserRepository(currentContext);
            this._commentRepository = new CommentRepository(currentContext);
        }
        ///<summary>
        /// Returns a list of 100 submissions frrom the database as SubmissionListViewModel collection
        /// </summary> 
        /// <param name="skipNrOfSubmissions"> Used for paging, when user presses next page send them the next hundred pages</param>
        ///<returns>A collection of SubmissionListViewModel objects</returns>
        public List<SubmissionListViewModel> Get100Submissions(int skipNrOfSubmissions)
        {
            //TODO: add sorting based on amount of votes and how controversial
            List<SubmissionListViewModel> submissions = this._submissionRepository.Query()
                .OrderBy(sub => sub.Upvotes)
                .Skip(skipNrOfSubmissions)
                .Take(100)
                .Select(submissionEntity => new SubmissionListViewModel
                {
                    Id = submissionEntity.Id,
                    Title = submissionEntity.Title,
                    Upvotes = submissionEntity.Upvotes,
                    Downvotes = submissionEntity.Downvotes,
                    AuthorId = submissionEntity.Author.Id
                }).ToList();
            return submissions;
        }

        /// <summary>
        ///     Inserts the submission in the database
        /// </summary>
        /// <param name="createSubmissionViewModel">The submission View Model</param>
        /// <returns></returns>
        public Guid InsertSubmission(CreateSubmissionViewModel createSubmissionViewModel)
        {

            //Create submission based on the viewmodel sent by the web client 
            Submission submissionEntity = new Submission
            {
                Id = Guid.NewGuid(),
                Title = createSubmissionViewModel.Title,
                Content = createSubmissionViewModel.Content,
                Upvotes = 0,        //submissions start with 0 votes
                Downvotes = 0,
               
                
            };

            //Get the author frorm the repo
            submissionEntity.Author = this._userRepository.GetUserById(createSubmissionViewModel.AuthorId);


            //add the submission to the DbSet of students with the state Added
            this._submissionRepository.AddSubmission(submissionEntity);

            //Save the current changes to the database
            this._submissionRepository.SaveChanges();

            return submissionEntity.Id;
      }
        /// <summary>
        /// Removes the submission with the given submissionId from the database
        /// </summary>
        /// <param name="submissionId"></param>
        public void DeleteSubmission(Guid submissionId)
        {
            Submission submissionToDelete = this._submissionRepository.Query()
                .Where(submission => submission.Id == submissionId)
                .FirstOrDefault(); 
                
            if(submissionToDelete == null)
            {
                throw new InvalidOperationException($"Submission with {submissionId} not found");
            }
            this._submissionRepository.DeleteSubmission(submissionToDelete);

            this._submissionRepository.SaveChanges();

        }
        /// <summary>
        /// Updates existing submission
        /// </summary>
        /// <param name="submissionViewModel">The submission view model</param>
        public void UpdateSubmission(EditSubmissionViewModel submissionViewModel)
        {
            Submission submissionEntityToUpdate = this._submissionRepository.GetSubmissionById(submissionViewModel.SubmissionId);
            submissionEntityToUpdate.Title = submissionViewModel.Title;
            submissionEntityToUpdate.Content = submissionViewModel.Content;

            this._submissionRepository.SaveChanges();
        }

        /// <summary>
        /// Returns the submission  with the given id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The student view model object</returns>
        public SubmissionViewModel GetSubmissionById(Guid id)
        {
            Submission submissionEntity = this._submissionRepository.Query()
                .Where(sub => sub.Id == id)
                .FirstOrDefault();
            if(submissionEntity == null)
            {
                throw new InvalidOperationException($"Submision with id {id} not found");
            }
            List<CommentViewModel> Comments = _commentRepository.Query()
                .Where(comm => comm.ParentSubmission == submissionEntity)
                .Select(commentEntity => new CommentViewModel
                {
                    Id = commentEntity.Id,
                    Text = commentEntity.Text,
                    Upvotes = commentEntity.Upvotes,
                    Downvotes = commentEntity.Downvotes,
                    AuthorName = commentEntity.Author.Name,
                    AuthorId = commentEntity.Author.Id,
                    NrOfReplies = commentEntity.NrOfReplies
                }).ToList();
            return new SubmissionViewModel
            {
                Id = submissionEntity.Id,
                Content = submissionEntity.Content,
                Title = submissionEntity.Title,
                Upvotes = submissionEntity.Upvotes,
                Downvotes = submissionEntity.Downvotes,
                AuthorName = submissionEntity.Author.Name,
                AuthorId = submissionEntity.Author.Id,
                Comments = Comments

            };

        }


     }

}
    





