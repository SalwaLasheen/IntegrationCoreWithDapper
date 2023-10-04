using Dapper;
using EFCoreVsDapper.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EFCoreVsDapper.Repos
{
    public class DapperRepository
    {
        public List<Employee> GetEmployeesWithDapper()
        {
            using (IDbConnection db = new SqlConnection(@"Server=localhost; Database=DemoDb; Trusted_Connection=True;Encrypt=false; MultipleActiveResultSets=true"))
            {
                return db.Query<Employee>
                ($"select top 600 * from Employees").ToList();

            }
        }
    }
}
