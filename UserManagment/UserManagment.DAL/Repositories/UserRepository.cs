using System.Linq;
using System.Data.Entity;
using UserManagment.DAL.Contexts;
using UserManagment.Domain.Interfaces.Repositories;
using UserManagment.Domain.Models;

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
    }
}
