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
    public class VoteService
    {
        private readonly VoteRepository _voteRepository;
        
        public VoteService()
        {
            UnpopularOpinionsDbContext dbContext = new UnpopularOpinionsDbContext();
            this._voteRepository = new VoteRepository(dbContext);
        }

        public Guid InsertVote(VoteViewModel voteModel)
        {
            Vote voteEntity = new Vote
            {
                Id = voteModel.Id,
                VoterId = voteModel.VoterId,
                VoteType = voteModel.VoteType,
                VoteValue = voteModel.VoteValue,
                SubOrCommId = voteModel.SubOrCommId
            };
            this._voteRepository.AddVote(voteEntity);
            this._voteRepository.SaveChanges();
            return voteEntity.Id;
        }
        public void UpdateVote(VoteViewModel voteModel)
        {
            Vote voteEntityToUpdate = this._voteRepository.GetVoteById(voteModel.Id);
            voteEntityToUpdate.VoteValue = voteModel.VoteValue;

            this._voteRepository.SaveChanges();
        }
        public VoteViewModel GetVoteBySubmissionAndVoterId(Guid SubmissionId, Guid VoterId)
        {
            Vote voteEntity = this._voteRepository.Query()
                .Where(vote => vote.VoteType == 0)
                .Where(vote => vote.VoterId == VoterId)
                .Where(vote => vote.SubOrCommId == SubmissionId)
                .FirstOrDefault();
            VoteViewModel voteModel = new VoteViewModel
            {
                Id = voteEntity.Id,
                VoterId = voteEntity.VoterId,
                VoteType = voteEntity.VoteType,
                VoteValue = voteEntity.VoteValue,
                SubOrCommId = voteEntity.SubOrCommId
            };
            return voteModel;

        }

        public VoteViewModel GetVoteByCommentAndVoterId(Guid CommentId, Guid VoterId)
        {
            Vote voteEntity = this._voteRepository.Query()
                .Where(vote => vote.VoteType == 0)
                .Where(vote => vote.VoterId == VoterId)
                .Where(vote => vote.SubOrCommId == CommentId)
                .FirstOrDefault();
            VoteViewModel voteModel = new VoteViewModel
            {
                Id = voteEntity.Id,
                VoterId = voteEntity.VoterId,
                VoteType = voteEntity.VoteType,
                VoteValue = voteEntity.VoteValue,
                SubOrCommId = voteEntity.SubOrCommId
            };
            return voteModel;

        }
        
    }
}
