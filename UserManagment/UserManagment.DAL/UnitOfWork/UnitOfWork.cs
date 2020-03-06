using UserManagment.DAL.Contexts;
using UserManagment.Domain.Interfaces.Repositories;
using UserManagment.Domain.Interfaces.UnitOfWork;

namespace UserManagment.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserStoryContext _context;

        public UnitOfWork(
            UserStoryContext context,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IJobRepository jobRepository)
        {
            _context = context;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            JobRepository = jobRepository;
        }

        public IUserRepository UserRepository { get; }

        public IRoleRepository RoleRepository { get; }

        public IJobRepository JobRepository { get; }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
