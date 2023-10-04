using BenchmarkDotNet.Attributes;
using DataAccessLayer.Application.Dtos;
using DataAccessLayer.Application.Repositories.Interface;

namespace Minimal_API.Endpoints
{
    public static class EmployeeEndPoint
    {

        public static void MapEmployeeEndPoints(this IEndpointRouteBuilder builder)
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


        [Benchmark]
        private static void EndPointsOfDapper(RouteGroupBuilder dapperGroup)
        {
            // get all employees
            dapperGroup.MapGet("GetAllEmployees", async (IDapperRepository<EmployeeInfo> dapperRepository) =>
            {
                return Results.Ok(await dapperRepository.Dapper_GetAllEmployeesAsync());
            });


            // Get by employee id
            dapperGroup.MapGet("GetEmployeeById/{id}", async (int employeeId, IDapperRepository<EmployeeInfo> dapperRepository) =>
            {

                var employees = await dapperRepository.Dapper_GetEmployeeByIdAsync(employeeId);
                return employees is not null ? Results.Ok(employees) : Results.NotFound();
            });


        }
        private static void EndPointsOfCore(RouteGroupBuilder coreGroup)
        {
            // get all employees
            coreGroup.MapGet("GetAllEmployees", async (ICoreRepository<EmployeeInfo> coreRepository) =>
            {
                return Results.Ok(await coreRepository.Core_GetAllEmployeesAsync());
            });


            // Get by employee id
            coreGroup.MapGet("GetEmployeeById/{id}", async (int employeeId, ICoreRepository<EmployeeInfo> coreRepository) =>
            {
                var employees = await coreRepository.Core_GetEmployeeByIdAsync(employeeId);
                return employees is not null ? Results.Ok(employees) : Results.NotFound();
            });
        }
        private static void EndPointsOfCompiledQuery(RouteGroupBuilder compiledQuery)
        {
            // get all employees
            compiledQuery.MapGet("GetAllEmployees", (ICompiledRepository<EmployeeInfo> compiledRepository) =>
            {
                return Results.Ok(compiledRepository.Compiled_GetAllEmployeesAsync());
            });


            // Get by employee id
            compiledQuery.MapGet("GetEmployeeById/{id}", async (int employeeId, ICompiledRepository<EmployeeInfo> compiledRepository) =>
            {
                var employees = await compiledRepository.Compiled_GetEmployeeByIdAsync(employeeId);
                return employees is not null ? Results.Ok(employees) : Results.NotFound();
            });


        }
        private static void EndPointsOfCoreAsDapper(RouteGroupBuilder coreAsDapper)
        {
            // get all employees
            coreAsDapper.MapGet("GetAllEmployees", async (ICoreAsDapperRepository<EmployeeInfo> coreAsDapperRepository) =>
                {
                    return Results.Ok(await coreAsDapperRepository.CoreAsDapper_GetAllEmployeesAsync());
                });


            // Get by employee id
            coreAsDapper.MapGet("GetEmployeeById/{id}", (int id, ICoreAsDapperRepository<EmployeeInfo> coreAsDapperRepository) =>
                {

                    var employees = coreAsDapperRepository.CoreAsDapper_GetEmployeeByIdAsync(id).Result;
                    return employees is not null ? Results.Ok(employees) : Results.NotFound();
                });



        }
    }
}
