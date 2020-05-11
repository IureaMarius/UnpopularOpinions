using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Vote
    {

        public Guid Id { get; set; }
        public Guid SubOrCommId{ get; set; }
        public int VoteValue { get; set; } // -1 for downvote, 0 for not voted and 1 for upvote

        public int VoteType { get; set; } //0 comment vote or 1 submission vote
        public Guid VoterId { get; set; }
        //when a vote is cast the program will check if the voter has already voted
        //and manage the situation properly

    }
}

