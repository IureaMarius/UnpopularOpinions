using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Submission
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        #region [ Navigation Properties]
        //This Icollection will only have the top level comments, parse the replies to get the rest of them
        public ICollection<Comment> Comments { get; set; }
        public User Author { get; set; }
        #endregion
    }
}
