using DeptSample.Data.Models;

namespace DeptSample.Business.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllAsync();
        Task<List<Department>> GetTreeAsync();//
        Task<Department?> GetByIdAsync(int id);//
        Task CreateAsync(Department dept);
        Task UpdateAsync(Department dept);
        Task DeleteAsync(int id);
        Task<List<Department>> GetAncestorsAsync(int id);
        Task<List<Department>> GetDescendantsAsync(int id);
    }
}