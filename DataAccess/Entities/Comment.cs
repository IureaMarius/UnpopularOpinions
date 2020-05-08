using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Comment
    {

        public Guid Id { get; set; }
        public string Text { get; set; }

        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public int NrOfReplies { get; set; }
        #region [ Navigation Properties ]
        //a comment will either be a top level post(commented directly on a submission or a reply to an existing comment
        //so either ParentSubmission or ParentComment will actually have a value
        public Submission ParentSubmission { get; set; }
        public Comment ParentComment { get; set; }
        public User Author { get; set; }
        public ICollection<Comment> Replies { get; set; }
              #endregion



    }
}
