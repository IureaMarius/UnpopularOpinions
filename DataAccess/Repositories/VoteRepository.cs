using DataAccess.Context;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class VoteRepository
    {
        private readonly UnpopularOpinionsDbContext _dbContext;
        public VoteRepository(UnpopularOpinionsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        /// <summary>
        /// Query the database table Votes
        /// </summary>
        /// <returns>Queryable reference to the database table</returns>
        public IQueryable<Vote> Query()
        {
            return this._dbContext.Votes.AsQueryable();
        }
        public Vote GetVoteById(Guid id)
        {
            Vote foundVote = this._dbContext.Votes
                .Where(vote => vote.Id == id)
                .FirstOrDefault();
            if(foundVote == null)
            {
                throw new InvalidOperationException($"The vote with id {id} could not be found");
            }
            return foundVote;
        }
        public void AddVote(Vote vote)
        {
            this._dbContext.Votes.Add(vote);
        }
        public void DeleteVote(Vote vote)
        {
            this._dbContext.Votes.Remove(vote);
        }
        public void SaveChanges()
        {
            this._dbContext.SaveChanges();
            this._dbContext.Dispose();
        }

    }
}
