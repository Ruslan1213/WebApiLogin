using System.Collections.Generic;
using UserManagment.Domain.Interfaces.Services;
using UserManagment.Domain.Models;

namespace UserManagment.BLL.Services.RightsResolvers
{
    public class UserRightsResolver : IRigtsResolver
    {
        private readonly IUserService _userService;

        private readonly IJobService _jobService;

        public UserRightsResolver(IUserService userService, IJobService jobService)
        {
            _userService = userService;
            _jobService = jobService;
        }

        public IEnumerable<User> GetUsersByRole(string userName)
        {
            return new List<User> { _userService.GetByName(userName) };
        }

        public IEnumerable<Job> GetJobsByRole(string userName)
        {
            var user = _userService.GetByName(userName);

            if (user == null)
            {
                return null;
            }

            return _jobService.Filter(x => x.UserId == user.Id);
        }
    }
}
