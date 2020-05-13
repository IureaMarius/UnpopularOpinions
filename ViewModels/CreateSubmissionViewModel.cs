using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CreateSubmissionViewModel
    {
        public Guid AuthorId { get; set; }
        [Required(ErrorMessage ="Title is mandatory")]
        [StringLength(200,ErrorMessage ="Title has to be below 200 characters")]
        public string Title { get; set; }
        //content can be empty 
        [StringLength(9000,ErrorMessage ="Keep your submission under 9000 characters")]
        public string Content { get; set; }

    }
}
