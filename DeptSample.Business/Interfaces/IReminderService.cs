
using DeptSample.Data.Models;

namespace DeptSample.Business.Interfaces
{
    public interface IReminderService
    {
        Task<List<Reminder>> GetAllAsync();
        Task<Reminder?> GetByIdAsync(int id);
        Task CreateAsync(Reminder reminder);
        Task UpdateAsync(Reminder reminder);
        Task DeleteAsync(int id);
        Task<List<Reminder>> GetDueAsync(DateTime now);
        Task MarkAsSentAsync(int id);
       
    }
}
