using DeptSample.Business.Interfaces;
using DeptSample.Data;
using DeptSample.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeptSample.Controllers
{
    public class RemindersController : Controller
    {
        private readonly IReminderService _reminders;

        public RemindersController(IReminderService reminders)
        {
            _reminders = reminders;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _reminders.GetAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reminder model)
        {
            if (!ModelState.IsValid) return View(model);
            await _reminders.CreateAsync(model);
            return RedirectToAction(nameof(Index));
            
        }
    }
}