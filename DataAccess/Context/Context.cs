using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class UnpopularOpinionsDbContext : DbContext
    {
        public UnpopularOpinionsDbContext()
            : base("Server=DESKTOP-QKG4NPF\\SQLEXPRESS;Database=UnpopularOpinionsDbContext;Trusted_Connection=True;")
        {
        }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}
