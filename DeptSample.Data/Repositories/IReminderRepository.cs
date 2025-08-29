using DeptSample.Data.Models;

namespace DeptSample.Data.Repositories
{
    public interface IReminderRepository : IRepository<Reminder>
    {
        Task<IEnumerable<Reminder>> GetPendingRemindersAsync(DateTime now);
    }
}