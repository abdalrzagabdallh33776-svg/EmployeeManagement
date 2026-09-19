using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeeManagement
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            System.Data.Entity.Database.SetInitializer(new Data.DatabaseInitializer());
            App_Start.RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes);
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null) return;
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket == null) return;
            var identity = new FormsIdentity(ticket);
            Context.User = new GenericPrincipal(identity, new[] { ticket.UserData });
        }
    }
}
