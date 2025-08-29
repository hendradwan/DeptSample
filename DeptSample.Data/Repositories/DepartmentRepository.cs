using DeptSample.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DeptSample.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context) { _context = context; }

        public async Task AddAsync(Department entity) => await _context.Departments.AddAsync(entity);
        public void Delete(Department entity) => _context.Departments.Remove(entity);

        public async Task<IEnumerable<Department>> GetAllAsync() =>
            await _context.Departments.AsNoTracking().OrderBy(d => d.Name).ToListAsync();

        public async Task<Department?> GetByIdAsync(int id) =>
            await _context.Departments
                .Include(d => d.SubDepartments)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<IEnumerable<Department>> GetChildrenAsync(int parentId) =>
            await _context.Departments
                .Where(d => d.ParentDepartmentId == parentId)
                .AsNoTracking()
                .OrderBy(d => d.Name)
                .ToListAsync();

        public async Task<IEnumerable<Department>> GetRootsAsync() =>
            await _context.Departments
                .Where(d => d.ParentDepartmentId == null)
                .AsNoTracking()
                .OrderBy(d => d.Name)
                .ToListAsync();

        public async Task<List<Department>> LoadTreeAsync()
        {
            var roots = (await GetRootsAsync()).ToList();
            foreach (var r in roots)
                await LoadChildrenRecursive(r);
            return roots;
        }

        private async Task LoadChildrenRecursive(Department dept)
        {
            var children = await _context.Departments
                .Where(x => x.ParentDepartmentId == dept.Id)
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
            dept.SubDepartments = children;
            foreach (var c in children)
                await LoadChildrenRecursive(c);
        }

        public void Update(Department entity) => _context.Departments.Update(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}