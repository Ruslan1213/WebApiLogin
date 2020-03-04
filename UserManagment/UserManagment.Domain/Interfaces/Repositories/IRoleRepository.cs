using UserManagment.Domain.Models;

namespace UserManagment.Domain.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetByName(string name);
    }
}
