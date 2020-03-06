using UserManagment.Domain.Interfaces.Repositories;

namespace UserManagment.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IRoleRepository RoleRepository { get; }

        IJobRepository JobRepository { get; }

        void Commit();
    }
}
