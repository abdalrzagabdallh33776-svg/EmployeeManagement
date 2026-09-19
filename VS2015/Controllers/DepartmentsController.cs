using System.Linq;
using System.Web.Mvc;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Departments.Include("Employees").ToList());
        }

        public ActionResult Create()
        {
            return View("Form", new Department());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department item)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", item);
            }

            db.Departments.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var item = db.Departments.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View("Form", item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department item)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", item);
            }

            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var item = db.Departments.Find(id);
            if (item != null && !db.Employees.Any(e => e.DepartmentId == id))
            {
                db.Departments.Remove(item);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
