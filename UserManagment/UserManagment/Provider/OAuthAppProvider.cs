using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using UserManagment.Configuration.AutofacConfiguration;
using UserManagment.Domain.Interfaces.Services;

namespace UserManagment.Provider
{
    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        private IUserService _userService;

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) =>  context.Validated();            

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            _userService = AutofacServiceConfig.GetUserService();
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var form = await context.Request.ReadFormAsync();
            string roleId = form["roleId"];
            var user = _userService.GetByCredential(context.UserName, context.Password, int.Parse(roleId));

            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username or password or role is incorrect");
                return;
            }

            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.RoleId.ToString()));
            context.Validated(identity);
        }
    }
}