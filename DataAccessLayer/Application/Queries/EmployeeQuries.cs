namespace DataAccessLayer.Application.Queries
{
    public static class EmployeeQuries
    {
        public static string AllEmployees =>
        "SELECT  Top(50) dbo.Employees.EmployeeId, dbo.Employees.EmployeeName, dbo.Employees.PhoneNumber, dbo.Departments.DepartmentName, dbo.Jobs.JobName, dbo.Jobs.MinSalary\r\nFROM            dbo.Departments INNER JOIN\r\n                         dbo.Employees ON dbo.Departments.DepartmentId = dbo.Employees.DepartmentId INNER JOIN\r\n                         dbo.Jobs ON dbo.Employees.JobId = dbo.Jobs.JobId";

        public static string EmployeeById => "SELECT * FROM [Employees]  WHERE [EmployeeId] = @EmployeeId";

        public static string AddEmployee =>
            @"INSERT INTO [Employees] ([EmployeeName], [PhoneNumber], [DepartmentId], [JobId]) 
            VALUES (@EmployeeName, @PhoneNumber, @DepartmentId, @JobId)";

        public static string UpdateEmployee =>
         @"UPDATE [Employees] 
        SET [EmployeeName] = @EmployeeName,
        [PhoneNumber] = @PhoneNumber,
        [DepartmentId] =@DepartmentId,
        [JobId] = @JobId
        WHERE [EmployeeId] = @EmployeeId";

        public static string DeleteEmployee => "DELETE FROM [Employees] WHERE [EmployeeId] = @EmployeeId";

    }
}
