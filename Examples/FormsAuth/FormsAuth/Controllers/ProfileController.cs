using System.Web.Mvc;
using DoubleConfirmIdentity.Mvc5;

namespace DoubleConfirmIdentity.Examples.FormsAuth.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [DoubleConfirmIdentity]
        public ActionResult Secured()
        {
            return View();
        }
    }
}