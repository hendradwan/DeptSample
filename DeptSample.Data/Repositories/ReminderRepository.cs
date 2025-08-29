using DeptSample.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DeptSample.Data.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly AppDbContext _context;
        public ReminderRepository(AppDbContext context) { _context = context; }

        public async Task AddAsync(Reminder entity) => await _context.Reminders.AddAsync(entity);
        public void Delete(Reminder entity) => _context.Reminders.Remove(entity);

        public async Task<IEnumerable<Reminder>> GetAllAsync() =>
            await _context.Reminders.AsNoTracking().OrderBy(r => r.ReminderTime).ToListAsync();

        public async Task<Reminder?> GetByIdAsync(int id) =>
            await _context.Reminders.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

        public async Task<IEnumerable<Reminder>> GetPendingRemindersAsync(DateTime now) =>
            await _context.Reminders.AsNoTracking().Where(r => !r.IsSent && r.ReminderTime <= now).ToListAsync();

        public void Update(Reminder entity) => _context.Reminders.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}