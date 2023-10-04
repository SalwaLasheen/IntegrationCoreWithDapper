using EFCoreVsDapper.Context;
using EFCoreVsDapper.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreVsDapper.Repos
{
    public class EFRepository
    {
        public List<string> GetEmployeesNameWithEFCoreWithNoTracking()
        {
            using (var db = new ApplicationDbContext())
            {
                var employees = db.Employees.Take(100).Select(x=>x.EmployeeName).AsNoTracking().ToList();
                return employees.ToList();
            }
        }


    }
}
