
using DeptSample.Business.Interfaces;
using DeptSample.Data;
using DeptSample.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DeptSample.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departments;
        private readonly IWebHostEnvironment _env;

        public DepartmentsController(IDepartmentService departments, IWebHostEnvironment env)
        {
            _departments = departments;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var tree = await _departments.GetTreeAsync();

            return View(tree);
        }
        public async Task<IActionResult> Details(int id)
        {
            var item = await _departments.GetByIdAsync(id);
            if (item == null) return NotFound();

            ViewBag.Parents = await _departments.GetAncestorsAsync(id);

            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.AllDepartments = await _departments.GetTreeAsync();
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Create([Bind("Name,ParentDepartmentId")] Department department, IFormFile? logoFile)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _departments.GetTreeAsync();
                return View(department);
            }

            if (logoFile != null)
            {

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
                var ext = Path.GetExtension(logoFile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("logoFile", "Only image files (.jpg, .png, .gif, etc.) are allow.");
                    ViewBag.Departments = await _departments.GetTreeAsync();
                    return View(department);
                }

                var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsPath);
                var filePath = Path.Combine(uploadsPath, logoFile.FileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await logoFile.CopyToAsync(stream);
                department.LogoPath = "/uploads/" + logoFile.FileName;
            }

            await _departments.CreateAsync(department);
            return RedirectToAction(nameof(Index));
        }
    }
}
