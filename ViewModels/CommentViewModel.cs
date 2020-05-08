using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Text{ get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int NrOfReplies { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public List<CommentViewModel> Replies { get; set; }
    }
}
