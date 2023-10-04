using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class Department
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DepartmentId { get; set; }
    [StringLength(30)]
    public string DepartmentName { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
}
