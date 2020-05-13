using DataAccess.Context;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataAccess.Repositories
{
    public class SubmissionRepository
    {
        
        private readonly UnpopularOpinionsDbContext _dbContext;
        private readonly CommentRepository _commentRepository;
        public SubmissionRepository(UnpopularOpinionsDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._commentRepository = new CommentRepository(dbContext);
        }
        
        /// <summary>
        /// Query the database table Submissions
        /// </summary>
        /// <returns>Queryable reference to the database table</returns>
        public IQueryable<Submission> Query()
        {
            return this._dbContext.Submissions.AsQueryable();
        }

        /// <summary>
        /// Get a submission based on it's Id
        /// </summary>
        /// <param name="id">The id of the requested submission</param>
        /// <returns>The submission object</returns>
        public Submission GetSubmissionById(Guid id)
        {
            Submission submission = this._dbContext.Submissions
                .Include(sub => sub.Author)
                .Include(sub => sub.Comments)
                .Where(stud => stud.Id == id)
                .FirstOrDefault();

            if(submission == null)
            {
                throw new InvalidOperationException($"Submission with id {id} could not be found");
            }

            return submission;

        }
        /// <summary>
        /// Add a submission to the in-memory collection
        /// </summary>
        /// <param name="sub">Submission to be removed</param>
        public void AddSubmission(Submission sub)
        {
            this._dbContext.Submissions.Add(sub);
        }
        /// <summary>
        /// Delete a submission from the in-memory collection and all the top level comments on that submission
        /// </summary>
        /// <param name="sub">Submission to be removed</param>
        public void DeleteSubmission(Submission sub)
        {
            if(sub.Comments!=null)
            foreach (Comment reply in sub.Comments.ToList())
                this._commentRepository.DeleteComment(reply);

            this._dbContext.Submissions.Remove(sub);

        }
        /// <summary>
        /// Save all the in-memory changes to the database and close connection
        /// </summary>
        public void SaveChanges()
        {
            this._dbContext.SaveChanges();
            this._dbContext.Dispose();

        }
    }
}
