using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace UserManagment.Extensions
{
    public static class ApiControllerExtension
    {
        public static string GetRole(this ApiController apiController)
        {
            var identity = (ClaimsIdentity)apiController.User.Identity;

            return identity
                .Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .First()
                .Value;
        }

        public static string GetModelErrors(this ApiController apiController)
        {
            return string
                .Join("; ", apiController.ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
        }
    }
}