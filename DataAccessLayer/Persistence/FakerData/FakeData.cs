using DataAccessLayer.Persistence.Context;

namespace DataAccessLayer.Persistence.FakerData
{
    public class FakeData
    {
        public static JobFaker jobFaker = new();
        public static DepartmentFaker departmentFaker = new();
        public static  EmployeeFaker employeeFaker = new();
        public static void GenerateFakerData(ApplicationDbContext context)
        {
            var checkIfDataExist = context.Employees.Any();
            if (!checkIfDataExist)
            {
                using var transaction = new System.Transactions.TransactionScope();

                var jobs = jobFaker.GenerateFakeJobs(10);
                context.Jobs.AddRange(jobs);
                context.SaveChanges();

                var departments = departmentFaker.GenerateFakeDepartments(50);
                context.Departments.AddRange(departments);
                context.SaveChanges();

                var employees = employeeFaker.GenerateFakeEmployees(50, jobs.FirstOrDefault(), departments.First());
                 context.Employees.AddRange(employees);
                 context.SaveChanges();


                transaction.Complete();
            }
        }



    }
}
