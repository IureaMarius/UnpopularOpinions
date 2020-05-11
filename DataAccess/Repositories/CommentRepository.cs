using DataAccess.Context;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CommentRepository
    {
        private readonly UnpopularOpinionsDbContext _dbContext;
        public CommentRepository(UnpopularOpinionsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        /// <summary>
        /// Query the database table Comments
        /// </summary>
        /// <returns>Queryable reference to the database table</returns>
        public IQueryable<Comment>Query()
        {
            return this._dbContext.Comments.AsQueryable();
        }
        
        /// <summary>
        /// Get a comment based on it's Id
        /// </summary>
        /// <param name="Id">The id of the requested comment</param>
        /// <returns>The comment object</returns>

        public Comment GetCommentById(Guid Id)
        {
            Comment comment = _dbContext.Comments
                .Where(comm => comm.Id == Id)
                .Include(comm => comm.Author)
                .Include(comm => comm.Replies)
                .FirstOrDefault();
            
            if(comment==null)
            {
                throw new InvalidOperationException($"The comment with id {Id} could not be found");
            }

            return comment;
        }
        /// <summary>
        /// Adds a comment to the in-memory collection 
        /// </summary>
        /// <param name="comment">The comment to be added</param>
        public void AddComment(Comment comment)
        {
            this._dbContext.Comments.Add(comment);
        }
        /// <summary>
        /// Deletes a comment from the in-memory collection (sets teh given student entity to Deleted state) 
        /// </summary>
        /// <param name="comment">The comment to be deleted</param>
        public void DeleteComment(Comment comment)
        {
            if(comment.Replies!=null)
            foreach (Comment reply in comment.Replies.ToList())
                DeleteComment(reply);

            this._dbContext.Comments.Remove(comment);

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
