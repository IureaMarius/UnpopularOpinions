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
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly CommentRepository _commentRepository;
        private readonly SubmissionRepository _submissionRepository;
        public UserService()
        {
            UnpopularOpinionsDbContext _dbContext = new UnpopularOpinionsDbContext();
            _userRepository = new UserRepository(_dbContext);
            _commentRepository = new CommentRepository(_dbContext);
            _submissionRepository = new SubmissionRepository(_dbContext);
        }
        /// <summary>
        /// Inserts user in the database
        /// </summary>
        /// <param name="userModel">The model of the user to be inserted</param>
        /// <returns>The user's id</returns>
        public Guid InsertUser(CreateUserViewModel userModel)
        {
            User userEntity = new User
            {
                Id = userModel.Id,
                Name = userModel.Name
            };
            this._userRepository.AddUser(userEntity);
            this._userRepository.SaveChanges();

            return userEntity.Id;

        }
        public CreateUserViewModel GetUserById(Guid id)
        {
            User userEntity = this._userRepository.GetUserById(id);
            CreateUserViewModel userModel = new CreateUserViewModel
            {
                Id = userEntity.Id,
                Name = userEntity.Name
            };
            return userModel;
        }
        /// <summary>
        /// Get a list of 100 comments made by user with id
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <param name="skipNrOfComments">Nr of comments to skip (pagination)</param>
        /// <returns>The ViewModel with the user's information and his comments</returns>
        public UserCommentsViewModel Get100UserCommentsById(Guid id, int skipNrOfComments)
        {
            User userEntity = _userRepository.GetUserById(id);
            if(userEntity==null)
            {
                throw new InvalidOperationException($"User with id {id} not found");
            }

            UserCommentsViewModel userModel = new UserCommentsViewModel
            {
                Name = userEntity.Name,
                Id = userEntity.Id
            };
            List<CommentViewModel> Comments = _commentRepository.Query()
                .Where(comm => comm.Author.Id == id)
                .OrderBy(comm => Math.Abs(comm.Upvotes-comm.Downvotes))
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

            userModel.Comments = Comments;
            return userModel;

        }

        /// <summary>
        /// Get a List of 100 submissions made by user with id
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <param name="skipNrOfSubmissions">Nr of submissions to skip (pagination)</param>
        /// <returns>The ViewModel with the user's information and his submissions</returns>
        public UserSubmissionsViewModel Get100UserSubmissionsById(Guid id,int skipNrOfSubmissions)
        {
            User userEntity = _userRepository.GetUserById(id);

            UserSubmissionsViewModel userModel = new UserSubmissionsViewModel
            {
                Name = userEntity.Name,
                Id = userEntity.Id
            };
            List<SubmissionListViewModel> Submissions= _submissionRepository.Query()
                .Where(sub => sub.Author.Id == id)
                .OrderBy(sub => Math.Abs(sub.Upvotes-sub.Downvotes))
                .Skip(skipNrOfSubmissions)
                .Take(100)
                .Select(subEntity => new SubmissionListViewModel
                {
                    Id = subEntity.Id,
                    Title = subEntity.Title,
                    Content = subEntity.Content,
                    AuthorId = subEntity.Author.Id,
                    AuthorName = subEntity.Author.Name,
                    Upvotes = subEntity.Upvotes,
                    Downvotes = subEntity.Downvotes
                }).ToList();

            userModel.Submissions= Submissions;
            return userModel;

        }
    }
}
