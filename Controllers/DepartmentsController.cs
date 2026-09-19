using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers;

public class DepartmentsController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index() => View(await context.Departments.Include(d => d.Employees).ToListAsync());
    public IActionResult Create() => View();
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Department department)
    {
        if (!ModelState.IsValid) return View(department);
        context.Add(department); await context.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id)
    {
        var item = await context.Departments.FindAsync(id); return item == null ? NotFound() : View(item);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Department department)
    {
        if (id != department.Id) return NotFound();
        if (!ModelState.IsValid) return View(department);
        context.Update(department); await context.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await context.Departments.Include(d => d.Employees).FirstOrDefaultAsync(d => d.Id == id);
        if (item == null) return NotFound();
        if (item.Employees.Count > 0) { TempData["Error"] = "لا يمكن حذف قسم يحتوي على موظفين."; return RedirectToAction(nameof(Index)); }
        context.Remove(item); await context.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
}
