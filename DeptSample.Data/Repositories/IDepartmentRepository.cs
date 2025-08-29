using DeptSample.Data.Models;

namespace DeptSample.Data.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<IEnumerable<Department>> GetChildrenAsync(int parentId);
        Task<IEnumerable<Department>> GetRootsAsync();
        Task<List<Department>> LoadTreeAsync();
    }
}