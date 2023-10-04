using AutoMapper;
using BenchmarkDotNet.Attributes;
using DataAccessLayer.Application.Dtos;
using DataAccessLayer.Application.Repositories.Interface;
using DataAccessLayer.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DataAccessLayer.Application.Repositories
{
    public class CoreAsDapperEmployeeRepository : ICoreAsDapperRepository<EmployeeInfo>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public record class EmployeeDto(int EmployeeId,string EmployeeName,string PhoneNumber);
        public CoreAsDapperEmployeeRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CoreAsDapper_CreateNewEmployeeAsync(EmployeeInfo employee)
        {
            await _context.Database.ExecuteSqlAsync(
                         $"INSERT INTO [Employee] ([EmployeeId],[EmployeeName], [PhoneNumber]) VALUES ({employee.EmployeeId},{employee.EmployeeName}, {employee.PhoneNumber}"
                 , CancellationToken.None);

        }

        public async Task CoreAsDapper_DeleteEmployeeAsync(int id)
        {

            await _context.Database.ExecuteSqlAsync($"Delete from Employees WHERE [EmployeeId] = {id}");
        }
        [Benchmark]
        public async Task<List<EmployeeInfo>> CoreAsDapper_GetAllEmployeesAsync()
        {
            FormattableString queryRelated = $"SELECT TOP(100) dbo.Employees.EmployeeName,dbo.Employees.EmployeeId,dbo.Employees.PhoneNumber, dbo.Departments.DepartmentName, dbo.Jobs.JobTitle, dbo.Jobs.Salary FROM dbo.Departments INNER JOIN                        dbo.Employees ON dbo.Departments.DepartmentId = dbo.Employees.DepartmentId INNER JOIN  dbo.Jobs ON dbo.Employees.JobId = dbo.Jobs.JobId";

            FormattableString query = $"select top 600 * from Employees";

            var result = await _context.Database.SqlQuery<EmployeeDto>(query).ToListAsync();
            var listofEmployeeInfo=new List<EmployeeInfo>();
            foreach (var emp in result)
            {
                listofEmployeeInfo.Add(new EmployeeInfo
                {
                    EmployeeId = emp.EmployeeId,
                    EmployeeName = emp.EmployeeName,
                    PhoneNumber = emp.PhoneNumber,
                });
            }
            return listofEmployeeInfo.ToList();

        }
        [Benchmark]
        public async Task<EmployeeInfo> CoreAsDapper_GetEmployeeByIdAsync(int id)
        {

            FormattableString query = $"SELECT TOP(100) dbo.Employees.EmployeeName,dbo.Employees.EmployeeId,dbo.Employees.PhoneNumber, dbo.Departments.DepartmentName, dbo.Jobs.JobTitle, dbo.Jobs.Salary FROM dbo.Departments INNER JOIN                        dbo.Employees ON dbo.Departments.DepartmentId = dbo.Employees.DepartmentId INNER JOIN  dbo.Jobs ON dbo.Employees.JobId = dbo.Jobs.JobId where  EmployeeId=608";

            var result = await _context.Database.SqlQuery<EmployeeDto>(query).AsNoTracking().FirstOrDefaultAsync();

            EmployeeInfo emp = new EmployeeInfo()
            {
                EmployeeId = result.EmployeeId,
                EmployeeName = result.EmployeeName,
                PhoneNumber = result.PhoneNumber,
            };
            return emp;
        }

        public Task CoreAsDapper_UpdateEmployeeAsync(EmployeeInfo emp, int id)
        {

            var result = _context.Database.ExecuteSqlAsync
                ($"UPDATE [Employees] SET [EmployeeName] = {emp.EmployeeName},[PhoneNumber] = {emp.PhoneNumber} WHERE [EmployeeId] = {id}");

            return result;
        }

    }
}
