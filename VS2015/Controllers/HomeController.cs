using System.Linq;
using System.Web.Mvc;
using EmployeeManagement.Data;
namespace EmployeeManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }
    }
}
