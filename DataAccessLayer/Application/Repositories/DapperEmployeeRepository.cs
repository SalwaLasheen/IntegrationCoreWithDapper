using BenchmarkDotNet.Attributes;
using Dapper;
using DataAccessLayer.Application.Dtos;
using DataAccessLayer.Application.Repositories.Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Persistence.Context;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayer.Application.Repositories
{
    public class DapperEmployeeRepository : IDapperRepository<EmployeeInfo>
    {
        private readonly SqlConnectionFactory _sqlConnectionFactory;
        public DapperEmployeeRepository(SqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<EmployeeInfo>> Dapper_GetAllEmployeesAsync()
        {
            //using var connection = _sqlConnectionFactory.Create();
            using (IDbConnection db = new SqlConnection(@"Server=localhost; Database=DemoDb; Trusted_Connection=True;Encrypt=false; MultipleActiveResultSets=true"))
            {
                const string query = $"select top 600 * from Employees";
                var allEmployees = await db.QueryAsync<EmployeeInfo>(query);
                return allEmployees.ToList();
            }
        }

        [Benchmark]
        public async Task<EmployeeInfo> Dapper_GetEmployeeByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.Create();

            const string queryRelated = $"SELECT TOP(100) dbo.Employees.EmployeeName,dbo.Employees.EmployeeId,dbo.Employees.PhoneNumber, dbo.Departments.DepartmentName, dbo.Jobs.JobTitle, dbo.Jobs.Salary FROM dbo.Departments INNER JOIN                        dbo.Employees ON dbo.Departments.DepartmentId = dbo.Employees.DepartmentId INNER JOIN  dbo.Jobs ON dbo.Employees.JobId = dbo.Jobs.JobId where EmployeeId=@EmployeeId";

            const string query = $"select top 600 * from Employees where EmployeeId=@EmployeeId";

            var result = await connection.QuerySingleOrDefaultAsync<EmployeeInfo>(query, new { EmployeeId = id });
            return result;
        }
        public async Task<int> Dapper_CreateNewEmployeeAsync(EmployeeInfo employees)
        {
            using var connection = _sqlConnectionFactory.Create();
            const string query = "INSERT INTO [Employees] ([EmployeeId],[EmployeeName], [PhoneNumber],[DepartmentId], [JobId]) VALUES (@EmployeeId,@EmployeeName, @PhoneNumber, @DepartmentId, @JobId)";
                    

            var result = await connection.ExecuteAsync(query, employees);
            return result;
        }
        public async Task<int> Dapper_UpdateEmployeeAsync(EmployeeInfo employees, int id)
        {
            using var connection = _sqlConnectionFactory.Create();
            employees.EmployeeId = id;

            var query = @"UPDATE [Employees] SET [EmployeeName] = @EmployeeName,[PhoneNumber] = @PhoneNumber,
                [DepartmentId] =@DepartmentId,[JobId] = @JobId WHERE [EmployeeId] = @EmployeeId";
            var result = await connection.ExecuteAsync(query, employees);
            return result;
        }
        public async Task<int> Dapper_DeleteEmployeeAsync(int id)
        {
            using var connection = _sqlConnectionFactory.Create();
            var query = @"Delete from Employees WHERE [EmployeeId] = @EmployeeId";
            var result = await connection.ExecuteAsync(query, new { EmployeeId = id });
            return result;
        }



    }
}
