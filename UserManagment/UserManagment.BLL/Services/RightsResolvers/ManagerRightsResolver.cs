using System.Collections.Generic;
using System.Linq;
using UserManagment.Domain.Enums;
using UserManagment.Domain.Interfaces.Services;
using UserManagment.Domain.Models;

namespace UserManagment.BLL.Services.RightsResolvers
{
    public class ManagerRightsResolver : IRigtsResolver
    {
        private readonly IUserService _userService;

        private readonly IJobService _jobService;

        public ManagerRightsResolver(IUserService userService, IJobService jobService)
        {
            _userService = userService;
            _jobService = jobService;
        }

        public IEnumerable<User> GetUsersByRole(string userName)
        {
            return _userService
                .Filter(x =>
                x.Role.Name == nameof(Roles.Manager) ||
                x.Role.Name == nameof(Roles.User));
        }

        public IEnumerable<Job> GetJobsByRole(string userName)
        {
            var users = GetUsersByRole(userName);
            var jobs = users.SelectMany(x => x.Jobs);

            return jobs;
        }
    }
}
