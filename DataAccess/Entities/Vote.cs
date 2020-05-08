using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Vote
    {

        public Guid SubmissionId { get; set; }
        public int VoteValue { get; set; }

        public int VoteType { get; set; }
        public Guid VoterId { get; set; }
        //when a vote is cast the program will check if the voter has already voted
        //and manage the situation properly

    }
}

