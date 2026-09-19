using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var user = db.Users.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password && x.IsActive);
            if (user == null)
            {
                ModelState.AddModelError("", "اسم المستخدم أو كلمة المرور ��ير صحيحة.");
                return View(model);
            }

            var ticket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now,
                model.RememberMe, user.Role, FormsAuthentication.FormsCookiePath);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(ticket)));

            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }

    public class LoginViewModel
    {
        [Required] public string UserName { get; set; }
        [Required, DataType(DataType.Password)] public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
