using DataAccess.Context;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly UnpopularOpinionsDbContext _dbContext;

        public UserRepository(UnpopularOpinionsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IQueryable<User> Query()
        {
            return this._dbContext.Users.AsQueryable();
        }
        /// <summary>
        /// Returns user with given id
        /// </summary>
        /// <param name="id"> id of the user</param>
        /// <returns>User object</returns>
        public User GetUserById(Guid id)
        {
            User foundUser = this._dbContext.Users
                .Where(user => user.Id == id)
                .FirstOrDefault();

            if (foundUser == null)
            {
                throw new InvalidOperationException($"The user with id {id} could not be found");
            }

            return foundUser;
        }
        /// <summary>
        /// Add user to the in-memory collection
        /// </summary>
        /// <param name="user">User to be added</param>
        public void AddUser(User user)
        {
            this._dbContext.Users.Add(user);
        }
        /// <summary>
        /// Deletes user from the in-memory collection
        /// </summary>
        /// <param name="user">The user to be deleted</param>
        public void DeleteUser(User user)
        {
            this._dbContext.Users.Remove(user);
        }
        /// <summary>
        /// Send all in-memory changes to the database and close connection
        /// </summary>
        public void SaveChanges()
        {
            this._dbContext.SaveChanges();
            this._dbContext.Dispose();
        }
    }
}
