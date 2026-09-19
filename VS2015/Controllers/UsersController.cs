using System.Linq;
using System.Web.Mvc;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    [Authorize(Roles = "مدير")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index() { return View(db.Users.OrderBy(x => x.UserName).ToList()); }
        public ActionResult Create() { return View(new User { IsActive = true, Role = "موظف" }); }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(User item)
        {
            if (db.Users.Any(x => x.UserName == item.UserName)) ModelState.AddModelError("UserName", "اسم المستخدم موجود مسبقاً.");
            if (!ModelState.IsValid) return View(item);
            db.Users.Add(item); db.SaveChanges(); return RedirectToAction("Index");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Toggle(int id)
        {
            var user = db.Users.Find(id);
            if (user != null && user.UserName != User.Identity.Name) { user.IsActive = !user.IsActive; db.SaveChanges(); }
            return RedirectToAction("Index");
        }
    }
}
