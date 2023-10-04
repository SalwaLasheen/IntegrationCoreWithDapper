using DataAccessLayer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Application.Repositories.CompareNewVersion
{
    public class EFRepository
    {
        public List<string> GetEmployeesNameWithEFCoreWithNoTracking()
        {
            using (var context = new ApplicationDbContext())
            {
                var employees = context.Employees.Take(100).Select(x => x.EmployeeName).AsNoTracking().ToList();
                return employees.ToList();
            }
        }


    }
}
