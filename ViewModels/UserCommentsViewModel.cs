using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserCommentsViewModel
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
