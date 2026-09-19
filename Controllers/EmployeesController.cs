using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers;

public class EmployeesController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index(string? search)
    {
        var query = context.Employees.Include(e => e.Department).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search)) query = query.Where(e => e.FullName.Contains(search) || e.Email.Contains(search));
        ViewBag.Search = search;
        return View(await query.OrderBy(e => e.FullName).ToListAsync());
    }
    public async Task<IActionResult> Create() { await LoadDepartments(); return View(); }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Employee employee)
    {
        if (!ModelState.IsValid) { await LoadDepartments(employee.DepartmentId); return View(employee); }
        context.Add(employee); await context.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id)
    {
        var item = await context.Employees.FindAsync(id); if (item == null) return NotFound();
        await LoadDepartments(item.DepartmentId); return View(item);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Employee employee)
    {
        if (id != employee.Id) return NotFound();
        if (!ModelState.IsValid) { await LoadDepartments(employee.DepartmentId); return View(employee); }
        context.Update(employee); await context.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var item = await context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);
        return item == null ? NotFound() : View(item);
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await context.Employees.FindAsync(id); if (item != null) { context.Remove(item); await context.SaveChangesAsync(); }
        return RedirectToAction(nameof(Index));
    }
    private async Task LoadDepartments(int? selected = null) => ViewBag.Departments = new SelectList(await context.Departments.OrderBy(d => d.Name).ToListAsync(), "Id", "Name", selected);
}
