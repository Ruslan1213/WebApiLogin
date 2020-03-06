using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using UserManagment.DAL.Contexts;
using UserManagment.Domain.Interfaces.Repositories;
using UserManagment.Domain.Models;

namespace UserManagment.DAL.Repositories
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private readonly UserStoryContext _context;

        public JobRepository(UserStoryContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Job> GetJobsByUserId(int userId)
        {
            return _context.Jobs.Where(x => x.UserId == userId);
        }

        public IEnumerable<Job> GetJobsByUserName(string userName)
        {
            return _context.Jobs.Where(x => x.User.Name == userName);
        }

        public override Job Get(int id)
        {
            return _context.Jobs.Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }
    }
}
