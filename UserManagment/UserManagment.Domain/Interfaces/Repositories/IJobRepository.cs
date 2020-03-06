using System.Collections.Generic;
using UserManagment.Domain.Models;

namespace UserManagment.Domain.Interfaces.Repositories
{
    public interface IJobRepository : IRepository<Job>
    {
        IEnumerable<Job> GetJobsByUserId(int userId);

        IEnumerable<Job> GetJobsByUserName(string userName);
    }
}
