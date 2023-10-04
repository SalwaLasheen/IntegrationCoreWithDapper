using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccessLayer.Models;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmployeeId { get; set; }

    [StringLength(30)]
    public string EmployeeName { get; set; }

    [StringLength(12)]
    public string PhoneNumber { get; set; }

    public int? DepartmentId { get; set; }

    public int? JobId { get; set; }

    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; }

    [ForeignKey("JobId")]
    public virtual Job Job { get; set; }
}