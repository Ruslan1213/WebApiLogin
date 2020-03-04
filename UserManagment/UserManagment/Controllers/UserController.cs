using Autofac.Features.Indexed;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using UserManagment.Domain.Enums;
using UserManagment.Domain.Interfaces.Services;
using UserManagment.Domain.Models;
using UserManagment.Models;

namespace UserManagment.Controllers
{
    public class UserController : ApiController
    {
        private readonly IIndex<Roles, IRigtsResolver> _states;

        private readonly IUserService _userService;

        private readonly IRoleService _roleService;

        private readonly IMapper _mapper;

        private IRigtsResolver _rigtsResolver;

        public UserController(
            IIndex<Roles, IRigtsResolver> states,
            IUserService userService,
            IRoleService roleService,
            IMapper mapper)
        {
            _mapper = mapper;
            _roleService = roleService;
            _userService = userService;
            _states = states;
        }

        [Route("api/register")]
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Register([FromBody]RegisterViewModel registerView)
        {
            if (!ModelState.IsValid)
            {
                string s = GetModelErrors();
                return BadRequest(s);
            }

            try
            {
                var user = _mapper.Map<User>(registerView);
                _userService.Insert(user);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("api/users")]
        public IHttpActionResult GetForAuthenticate()
        {
            var role = GetRole();

            if (role == null)
            {
                return BadRequest();
            }

            Roles enumRole = (Roles)Enum.Parse(typeof(Roles), role);
            _rigtsResolver = _states[enumRole];
            var users = _rigtsResolver.GetUsersByRole(User.Identity.Name);

            return Ok(GetUsers(users));
        }

        [HttpGet]
        [Route("api/roles")]
        public IEnumerable<Role> GetRoles()
        {
            return _roleService.GetAll();
        }

        private IEnumerable<UserViewModel> GetUsers(IEnumerable<User> users)
        {
            return users.Select(x => _mapper.Map<UserViewModel>(x));
        }

        private string GetRole()
        {
            var identity = (ClaimsIdentity)User.Identity;

            return identity
                .Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .First()
                .Value;
        }

        private string GetModelErrors()
        {
            return string
                .Join("; ", ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
        }
    }
}
