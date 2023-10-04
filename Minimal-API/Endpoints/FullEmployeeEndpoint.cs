using DataAccessLayer.Application.Repositories.Interface;
using DataAccessLayer.Models;

namespace Minimal_API.Endpoints
{
    public static class FullEmployeeEndpoint
    {

        public static void MapEmployeeFullEndPoints(this IEndpointRouteBuilder builder)
        {
            var dapperGroup = builder.MapGroup("DapperEmployees");

            EndPointsOfDapper(dapperGroup);

            var coreGroup = builder.MapGroup("CoreEmployees");
            EndPointsOfCore(coreGroup);

            var compiledQueryGroup = builder.MapGroup("CompiledEmployees");
            EndPointsOfCompiledQuery(compiledQueryGroup);

            var coreAsDapperGroup = builder.MapGroup("coreAsDapper");
            EndPointsOfCoreAsDapper(coreAsDapperGroup);

        }

        private static void EndPointsOfDapper(RouteGroupBuilder dapperGroup)
        {
            // get all employees
            dapperGroup.MapGet("GetAllEmployees", async (IDapperRepository<Employee> dapperRepository) =>
            {
                return Results.Ok(await dapperRepository.Dapper_GetAllEmployeesAsync());
            });


            // Get by employee id
            dapperGroup.MapGet("GetEmployeeById/{id}", async (int employeeId, IDapperRepository<Employee> dapperRepository) =>
            {

                var employees = await dapperRepository.Dapper_GetEmployeeByIdAsync(employeeId);
                return employees is not null ? Results.Ok(employees) : Results.NotFound();
            });


            // Post Employee
            dapperGroup.MapPost("PostEmployee", async (Employee employees, IDapperRepository<Employee> dapperRepository) =>
            {
                var employee = await dapperRepository.Dapper_CreateNewEmployeeAsync(employees);
                return Results.Ok(employee);
            });


            // update Employee
            dapperGroup.MapPut("UpdateEmployee/{id}", async (int employeeId, Employee employees, IDapperRepository<Employee> dapperRepository) =>
            {
                var employee = await dapperRepository.Dapper_UpdateEmployeeAsync(employees, employeeId);
                return Results.NoContent();
            });

            // update Employee
            dapperGroup.MapDelete("DeleteEmployee{id}", async (int employeeId, IDapperRepository<Employee> dapperRepository) =>
            {
                await dapperRepository.Dapper_DeleteEmployeeAsync(employeeId);
                return Results.NoContent();
            });
        }
        private static void EndPointsOfCore(RouteGroupBuilder coreGroup)
        {
            // get all employees
            coreGroup.MapGet("GetAllEmployees", async (ICoreRepository<Employee> coreRepository) =>
            {
                return Results.Ok(await coreRepository.Core_GetAllEmployeesAsync());
            });


            // Get by employee id
            coreGroup.MapGet("GetEmployeeById/{id}", async (int employeeId, ICoreRepository<Employee> coreRepository) =>
            {
                var employees = await coreRepository.Core_GetEmployeeByIdAsync(employeeId);
                return employees is not null ? Results.Ok(employees) : Results.NotFound();
            });


            // Post Employee
            coreGroup.MapPost("PostEmployee", async (Employee employees, ICoreRepository<Employee> coreRepository) =>
            {
                await coreRepository.Core_CreateNewEmployeeAsync(employees);
                return Results.Ok();
            });


            // update Employee
            coreGroup.MapPut("UpdateEmployee/{id}", async (int employeeId, Employee employees, ICoreRepository<Employee> coreRepository) =>
            {
                await coreRepository.Core_UpdateEmployeeAsync(employees, employeeId);
                return Results.NoContent();
            });

            // Delete Employee
            coreGroup.MapDelete("DeleteEmployee{id}", async (int employeeId, ICoreRepository<Employee> coreRepository) =>
            {
                await coreRepository.Core_DeleteEmployeeAsync(employeeId);
                return Results.NoContent();
            });
        }
        private static void EndPointsOfCompiledQuery(RouteGroupBuilder compiledQuery)
        {
            // get all employees
            compiledQuery.MapGet("GetAllEmployees", (ICompiledRepository<Employee> compiledRepository) =>
            {
                return Results.Ok(compiledRepository.Compiled_GetAllEmployeesAsync());
            });


            // Get by employee id
            compiledQuery.MapGet("GetEmployeeById/{id}", async (int employeeId, ICompiledRepository<Employee> compiledRepository) =>
            {
                var employees = await compiledRepository.Compiled_GetEmployeeByIdAsync(employeeId);
                return employees is not null ? Results.Ok(employees) : Results.NotFound();
            });


        }
        private static void EndPointsOfCoreAsDapper(RouteGroupBuilder coreAsDapper)
        {
            // get all employees
            coreAsDapper.MapGet("GetAllEmployees", (ICoreAsDapperRepository<Employee> coreAsDapperRepository) =>
            {
                return Results.Ok(coreAsDapperRepository.CoreAsDapper_GetAllEmployeesAsync());
            });


            // Get by employee id
            coreAsDapper.MapGet("GetEmployeeById/{id}", (int employeeId, ICoreAsDapperRepository<Employee> coreAsDapperRepository) =>
            {
                var employees = coreAsDapperRepository.CoreAsDapper_GetEmployeeByIdAsync(employeeId).Result;
                return employees is not null ? Results.Ok(employees) : Results.NotFound();
            });
            // Post Employee
            coreAsDapper.MapPost("PostEmployee", async (Employee employees, ICoreAsDapperRepository<Employee> coreAsDapper) =>
            {
                await coreAsDapper.CoreAsDapper_CreateNewEmployeeAsync(employees);
                return Results.Ok();
            });


            // update Employee
            coreAsDapper.MapPut("UpdateEmployee/{id}", async (int employeeId, Employee employees, ICoreAsDapperRepository<Employee> coreAsDapperRepository) =>
            {
                await coreAsDapperRepository.CoreAsDapper_UpdateEmployeeAsync(employees, employeeId);
                return Results.NoContent();
            });

            // Delete Employee
            coreAsDapper.MapDelete("DeleteEmployee{id}", async (int employeeId, ICoreAsDapperRepository<Employee> coreAsDapperRepository) =>
            {
                await coreAsDapperRepository.CoreAsDapper_DeleteEmployeeAsync(employeeId);
                return Results.NoContent();
            });


        }
    }
}
