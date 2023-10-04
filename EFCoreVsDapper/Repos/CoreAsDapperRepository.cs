using EFCoreVsDapper.Context;
using EFCoreVsDapper.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreVsDapper.Repos
{
    public class CoreAsDapperRepository
    {
        public List<Employee> GetEmployeesWithEFAsDapper()
        {
            using (var db = new ApplicationDbContext())
            {
                var employees = db.Employees.FromSql($"Select top 600 * from Employees").AsNoTracking().ToList();
                return employees;
              
            }
        }
    }
}
