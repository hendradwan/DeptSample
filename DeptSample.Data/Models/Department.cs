using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeptSample.Data.Models
{
    public class Department
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string? LogoPath { get; set; }

        public int? ParentDepartmentId { get; set; }

        [ForeignKey("ParentDepartmentId")]
        public Department? ParentDepartment { get; set; }

        public List<Department> SubDepartments { get; set; } = new();
    }
}
