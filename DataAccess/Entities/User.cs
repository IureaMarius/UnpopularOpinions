using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class User
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }

        #region [ Navigation Properties ]
        public ICollection<Submission> Submissions { get; set; }
        public ICollection<Comment> Comments { get; set; }
        #endregion
    }



}
