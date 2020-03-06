using System.Linq;
using System.Data.Entity;
using UserManagment.DAL.Contexts;
using UserManagment.Domain.Interfaces.Repositories;
using UserManagment.Domain.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace UserManagment.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserStoryContext _context;

        public UserRepository(UserStoryContext context) : base(context)
        {
            _context = context;
        }

        public User GetByCredential(string name, string password)
        {
            return _context
                .Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Name == name
                && x.Password == password);
        }

        public User GetByName(string name)
        {
            return _context
                .Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Name == name);
        }

        public override IEnumerable<User> Filter(Expression<Func<User, bool>> filter)
        {
            return _context.Users.Where(filter).Include(x => x.Jobs);
        }
    }
}
