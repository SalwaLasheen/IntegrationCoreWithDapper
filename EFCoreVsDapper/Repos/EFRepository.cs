using EFCoreVsDapper.Context;
using EFCoreVsDapper.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreVsDapper.Repos
{
    public class EFRepository
    {
        public List<Employee> GetEmployeesWithEF()
        {
            using (var db = new ApplicationDbContext())
            {
                var employees = db.Employees.Take(600).AsNoTracking().ToList();
                return employees.ToList();
            }
        }
    }
}
