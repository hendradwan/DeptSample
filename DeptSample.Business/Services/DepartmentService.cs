
using DeptSample.Business.Interfaces;
using DeptSample.Data;
using DeptSample.Data.Models;
using DeptSample.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeptSample.Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;
        public DepartmentService(IDepartmentRepository repo) { _repo = repo; }
        public async Task<List<Department>> GetAllAsync() => (await _repo.GetAllAsync()).ToList();
        public async Task<List<Department>> GetTreeAsync() => await _repo.LoadTreeAsync();
        public async Task<Department?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task<List<Department>> GetAncestorsAsync(int id)
        {
            var list = new List<Department>();
            var current = await _repo.GetByIdAsync(id);
            while (current != null && current.ParentDepartmentId.HasValue)
            {
                var parent = await _repo.GetByIdAsync(current.ParentDepartmentId.Value);
                if (parent == null) break;
                list.Add(parent);
                current = parent;
            }
            list.Reverse();
            return list;
        }
        public async Task CreateAsync(Department dept)
        {
            await _repo.AddAsync(dept);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department dept)
        {
            _repo.Update(dept);
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

        public async Task<List<Department>> GetDescendantsAsync(int id)
        {
            var result = new List<Department>();
            var root = await _repo.GetByIdAsync(id);
            if (root != null)
                await Collect(root, result);
            return result;
        }

        private async Task Collect(Department d, List<Department> acc)
        {
            var children = await _repo.GetChildrenAsync(d.Id);
            foreach (var c in children)
            {
                acc.Add(c);
                await Collect(c, acc);
            }
        }
    }
}
