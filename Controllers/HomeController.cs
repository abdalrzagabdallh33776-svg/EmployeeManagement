using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
    public IActionResult Error() => View();
}
