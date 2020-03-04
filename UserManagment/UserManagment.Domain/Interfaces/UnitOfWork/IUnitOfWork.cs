using UserManagment.Domain.Interfaces.Repositories;

namespace UserManagment.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IRoleRepository RoleRepository { get; }

        void Commit();
    }
}
