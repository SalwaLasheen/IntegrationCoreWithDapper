using AutoMapper;
using BenchmarkDotNet.Attributes;
using DataAccessLayer.Application.Dtos;
using DataAccessLayer.Application.Repositories.Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Application.Repositories
{
    public class CompiledEmployeeRepository : ICompiledRepository<EmployeeInfo>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public static readonly Func<ApplicationDbContext, int, Task<Employee>> GetEmployeeWithInfoById =
            EF.CompileAsyncQuery((ApplicationDbContext context, int id) =>
            context.Set<Employee>().Include(x => x.Department).Include(x => x.Job).SingleOrDefault(x => x.EmployeeId == id));

        public static readonly Func<ApplicationDbContext, int, Task<Employee>> GetEmployeeWithoutInfoById =
           EF.CompileAsyncQuery((ApplicationDbContext context, int id) =>
           context.Set<Employee>().SingleOrDefault(x => x.EmployeeId == id));

        public static readonly Func<ApplicationDbContext, List<Employee>> GetAllEmployeeswithInfo =
         EF.CompileQuery((ApplicationDbContext context) =>
            context.Set<Employee>().ToList());

        public static readonly Func<ApplicationDbContext, List<Employee>> GetAllEmployeesWithoutInfo =
   EF.CompileQuery((ApplicationDbContext context) =>
      context.Set<Employee>().AsNoTracking().ToList());


        public CompiledEmployeeRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [Benchmark]
        public List<EmployeeInfo> Compiled_GetAllEmployeesAsync()
        {
            var listEmployees = GetAllEmployeesWithoutInfo(_context).Take(600).ToList();
            var listOfEployeeInfo = _mapper.Map<List<Employee>, List<EmployeeInfo>>(listEmployees);
            
            return listOfEployeeInfo;
        }
        [Benchmark]
        public async Task<EmployeeInfo> Compiled_GetEmployeeByIdAsync(int id)
        {
            var employee = await GetEmployeeWithoutInfoById(_context, id);
            var employeeInfo = _mapper.Map<Employee, EmployeeInfo>(employee);


            /* var employee = await GetEmployeeWithInfoById(_context, id);
             employeeInfo.DepartmentName = employee.Department.DepartmentName;
             employeeInfo.JobTitle = employee.Job.JobTitle;
             employeeInfo.Salary = employee.Job.Salary;*/

            return employeeInfo;
        }


    }
}
