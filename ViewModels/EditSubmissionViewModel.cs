using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class EditSubmissionViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        //TODO: implement checking if the user has posted the submission which he is trying to edit
        public Guid AuthorId { get; set; }
        public Guid SubmissionId { get; set; }


    }
}
