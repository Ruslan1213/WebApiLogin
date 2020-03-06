using System.Collections.Generic;
using UserManagment.Domain.Models;

namespace UserManagment.Domain.Interfaces.Services
{
    public interface IRigtsResolver
    {
        IEnumerable<User> GetUsersByRole(string userName);

        IEnumerable<Job> GetJobsByRole(string userName);
    }
}
