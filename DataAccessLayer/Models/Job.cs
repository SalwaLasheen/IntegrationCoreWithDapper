using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccessLayer.Models;

public class Job
{
    public Job()
    {

        Employees = new List<Employee>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int JobId { get; set; }

    [StringLength(50)]
    public string JobTitle { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? Salary { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } 
}