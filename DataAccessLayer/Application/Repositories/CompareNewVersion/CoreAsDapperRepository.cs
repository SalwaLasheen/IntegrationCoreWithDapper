using DataAccessLayer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EFCoreVsDapper.Repos
{
    public class CoreAsDapperRepository
    {
        public List<string> GetEmployeesWithEFAsDapperFromSql()
        {
            using (var db = new ApplicationDbContext())
            {
                var employees = db.Employees.FromSql($"Select top 100 EmployeeName from Employees").Select(x => x.EmployeeName).AsNoTracking().ToList();
                return employees.ToList();

            }
        }
        public List<string> GetEmployeesWithEFAsDapperFromSqLROW()
        {
            using (var db = new ApplicationDbContext())
            {
                var employees = db.Employees.FromSqlRaw($"Select top 100 EmployeeName from Employees").AsNoTracking().Select(x => x.EmployeeName).ToList();
                return employees.ToList();

            }
        }
        public List<string> GetEmployeesWithEFAsDapperSQLQuery()
        {
            using (var db = new ApplicationDbContext())
            {
                var employeesName = db.Database.SqlQuery<string>($"Select top 100 EmployeeName from Employees").AsNoTracking().ToList();
                return employeesName;

            }
        }
        public List<string> GetEmployeesWithEFAsDapperSQLQueryRow()
        {
            using (var db = new ApplicationDbContext())
            {
                var employeesName = db.Database.SqlQueryRaw<string>($"Select top 100 EmployeeName from Employees").AsNoTracking().ToList();
                return employeesName;

            }
        }

    }
}
