using System.Collections.Generic;
using System.Linq;
using UserManagment.Domain.Enums;
using UserManagment.Domain.Interfaces.Services;
using UserManagment.Domain.Models;

namespace UserManagment.BLL.Services.RightsResolvers
{
    public class AdminRightsResolver : IRigtsResolver
    {
        private readonly IUserService _userService;

        public AdminRightsResolver(IUserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<User> GetUsersByRole(string userName)
        {
            return _userService.Filter(x => x.Role.Name == nameof(Roles.Admin));
        }

        public IEnumerable<Job> GetJobsByRole(string userName)
        {
            var users = GetUsersByRole(userName);
            var jobs = users.SelectMany(x => x.Jobs);

            return jobs;
        }
    }
}
