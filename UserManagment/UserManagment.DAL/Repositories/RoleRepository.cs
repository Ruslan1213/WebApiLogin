using System.Linq;
using System.Data.Entity;
using UserManagment.DAL.Contexts;
using UserManagment.Domain.Interfaces.Repositories;
using UserManagment.Domain.Models;

namespace UserManagment.DAL.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly UserStoryContext _context;

        public RoleRepository(UserStoryContext context) : base(context)
        {
            _context = context;
        }

        public Role GetByName(string name)
        {
            return _context
                .Roles
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Name == name);
        }
    }
}
