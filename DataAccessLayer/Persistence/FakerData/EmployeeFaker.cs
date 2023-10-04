using Bogus;
using DataAccessLayer.Models;

namespace DataAccessLayer.Persistence.FakerData
{
    public class EmployeeFaker : Faker<Employee>
    {
        public   List<Employee> GenerateFakeEmployees(int count, Job jobs, Department departments)
        {
            var id = 600;
            List<Employee> employees = new ();
            var employeeFaker = new Faker<Employee>()
                 .RuleFor(c => c.EmployeeId, f => ++id)
               .RuleFor(a => a.EmployeeName, f => f.Person.FullName)
               .RuleFor(a => a.PhoneNumber, f => f.Phone.PhoneNumberFormat())
               .RuleFor(c => c.DepartmentId, departments.DepartmentId)
               .RuleFor(c => c.JobId, jobs.JobId);
            employees.AddRange(employeeFaker.Generate(count));
            return employees;

        }

    }

}
