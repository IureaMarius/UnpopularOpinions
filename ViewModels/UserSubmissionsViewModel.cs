using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserSubmissionsViewModel
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public List<SubmissionListViewModel> Submissions{ get; set; }
    }
}

