using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CreateSubmissionViewModel
    {
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
