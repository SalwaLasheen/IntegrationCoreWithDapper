using AutoMapper;
using BenchmarkDotNet.Attributes;
using DataAccessLayer.Application.Dtos;
using DataAccessLayer.Application.Repositories.Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace DataAccessLayer.Application.Repositories
{
    public class CoreEmployeeRepository : ICoreRepository<EmployeeInfo>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CoreEmployeeRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }
        [Benchmark]
        public async Task<List<EmployeeInfo>> Core_GetAllEmployeesAsync()
        {
            var allEmployees = await _context.Employees.Take(600)
                 .Select(x => new EmployeeInfo
                 {                     
                     EmployeeId = x.EmployeeId,
                     EmployeeName = x.EmployeeName,
                     PhoneNumber = x.PhoneNumber,
                 }).ToListAsync();
            return allEmployees;
        }
        [Benchmark]
        public async Task<EmployeeInfo> Core_GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees.Select(x => new EmployeeInfo
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = x.EmployeeName,
                PhoneNumber = x.PhoneNumber,
            }).SingleOrDefaultAsync(x => x.EmployeeId == id);

            //ToDo:Related Related Data

            /*var employee = await _context.Employees.Include(x=>x.Department).Include(x=>x.Job).Select(x=> new EmployeeInfo
            {
                DepartmentName=x.Department.DepartmentName,
                EmployeeId=x.EmployeeId,    
                EmployeeName=x.EmployeeName,
                PhoneNumber=x.PhoneNumber,
                JobTitle=x.Job.JobTitle,
                Salary=x.Job.Salary
            }).SingleOrDefaultAsync(x => x.EmployeeId == id);*/
            return employee;
        }
        public async Task Core_CreateNewEmployeeAsync(EmployeeInfo employeeInfo)
        {
            var employee = _mapper.Map<EmployeeInfo, Employee>(employeeInfo);
            await _context.Employees.AddAsync(employee);
        }
        public async Task Core_UpdateEmployeeAsync(EmployeeInfo emp, int id)
        {
            var employee = await _context.Employees.Where(x => x.EmployeeId == id).FirstOrDefaultAsync();
            employee.PhoneNumber = emp.PhoneNumber;
            employee.EmployeeName = emp.EmployeeName;
            await _context.SaveChangesAsync();

        }
        public async Task Core_DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.Where(x => x.EmployeeId == id).FirstOrDefaultAsync();
            _context.Employees.Remove(employee);

        }

    }
}
