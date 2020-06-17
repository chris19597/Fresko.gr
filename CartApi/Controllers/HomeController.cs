using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CartApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            using (var db = new Context())
            {
                var crd = db.Cards.First(sh => id.Contains(sh.ID));
                return View(crd);
            }
        }

        public ActionResult SignOut()
        {
            var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            return Redirect("Login");
        }
    }
}