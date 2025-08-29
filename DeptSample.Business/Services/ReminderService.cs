
using DeptSample.Business.Interfaces;
using DeptSample.Data;
using DeptSample.Data.Models;
using DeptSample.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeptSample.Business.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _repo;

        public ReminderService(IReminderRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Reminder>> GetAllAsync() => (await _repo.GetAllAsync()).ToList();

        public async Task<Reminder?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task CreateAsync(Reminder reminder)
        {
            await _repo.AddAsync(reminder);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reminder reminder)
        {
            _repo.Update(reminder);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
            {
                _repo.Delete(entity);
                await _repo.SaveChangesAsync();
            }
        }

        public async Task<List<Reminder>> GetDueAsync(DateTime now) => (await _repo.GetPendingRemindersAsync(now)).ToList();

        public async Task MarkAsSentAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsSent = true;
                await _repo.SaveChangesAsync();
            }
        }

      
    }
}
