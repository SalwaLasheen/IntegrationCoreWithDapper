using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayer.Application.Repositories.CompareNewVersion
{
    public class DapperRepository
    {
        public List<string> GetEmployeesWithDapper()
        {
            using (IDbConnection db = new SqlConnection(@"Server=localhost; Database=DemoDb; Trusted_Connection=True;Encrypt=false; MultipleActiveResultSets=true"))
            {
                return db.Query<string>
                ($"select top 100 EmployeeName from Employees").ToList();

            }
        }
    }
}
