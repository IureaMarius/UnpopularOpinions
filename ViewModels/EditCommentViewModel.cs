using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class EditCommentViewModel
    {
        public string Text { get; set; }
        public Guid CommentId { get; set; }
        public Guid AuthorId { get; set; }//for auth verification
    }
}
