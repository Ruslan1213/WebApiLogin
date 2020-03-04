using System.Web.Mvc;

namespace UserManagment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();

        public ActionResult Main() => View();

        public ActionResult Login() => View();
    }
}
