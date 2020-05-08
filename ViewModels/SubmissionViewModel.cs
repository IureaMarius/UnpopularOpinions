using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class SubmissionViewModel
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public string AuthorName { get; set; }
        public Guid AuthorId { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}

