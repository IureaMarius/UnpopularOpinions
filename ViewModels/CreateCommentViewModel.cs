using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CreateCommentViewModel
    {
        public string Text { get; set; }
        public Guid AuthorId { get; set; }
        public Guid ParentSubmissionId { get; set; }
        public Guid ParentCommentId { get; set; }
    }
}
