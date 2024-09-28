using System.Diagnostics;
using EmployeeManagement.AppDbContexts;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Employee()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult AddEmployee(int? id)
        {
            if (id == null)
            {
                return View(new Employee()); // Return a new Employee model for adding
            }

            // For editing, fetch the existing employee
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee); // Pass the existing employee model to the view
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.Id == 0)
                {
                    // Add new employee
                    await _context.Employees.AddAsync(employee);
                }
                else
                {
                    // Update existing employee
                    _context.Employees.Update(employee);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Employee");
            }

            return View(employee); // If validation fails, show the same form with errors
        }

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Employee");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
