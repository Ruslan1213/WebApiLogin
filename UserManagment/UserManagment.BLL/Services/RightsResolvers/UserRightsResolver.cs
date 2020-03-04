using System.Collections.Generic;
using UserManagment.Domain.Interfaces.Services;
using UserManagment.Domain.Models;

namespace UserManagment.BLL.Services.RightsResolvers
{
    public class UserRightsResolver : IRigtsResolver
    {
        private readonly IUserService _userService;

        public UserRightsResolver(IUserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<User> GetUsersByRole(string userName)
        {
            return new List<User> { _userService.GetByName(userName) };
        }
    }
}
