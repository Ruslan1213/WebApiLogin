using UserManagment.Domain.Models;

namespace UserManagment.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByName(string name);

        User GetByCredential(string name, string password);
    }
}
