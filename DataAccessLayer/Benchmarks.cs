using AutoMapper;
using BenchmarkDotNet.Attributes;
using DataAccessLayer.Application.Repositories;
using DataAccessLayer.Persistence.Context;

namespace DataAccessLayer
{
    [GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    [MemoryDiagnoser]
    public class Benchmarks
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        private SqlConnectionFactory _Connection;
        public Benchmarks()
        {
            _context = new ApplicationDbContext();

        }
        /* [GlobalSetup]
         public void Setup()
         {
              _Connection = new SqlConnectionFactory(_context.Database.GetConnectionString());
         }
         [GlobalCleanup]
         public void Cleanup()
         {
             _context.Dispose();
         }*/

        [BenchmarkCategory("slow"), Benchmark]
        public void Compiled_GetEmployees()
        {
            var c = new CompiledEmployeeRepository(_context, _mapper).Compiled_GetAllEmployeesAsync();

        }
        [BenchmarkCategory("slow"), Benchmark(Baseline = true)]
        public void Core_GetEmployees()
        {
            var employeeId = 608;
            var c = new CoreEmployeeRepository(_context, _mapper).Core_GetAllEmployeesAsync();

        }
        [BenchmarkCategory("fast"), Benchmark(Baseline = true)]
        public void CoreAsDapper_GetEmployees()
        {
            var employeeId = 608;
            var c = new CoreAsDapperEmployeeRepository(_context, _mapper).CoreAsDapper_GetAllEmployeesAsync();

        }
        [BenchmarkCategory("fast"), Benchmark]
        public void Dapper_GetEmployees()
        {

            _Connection = new SqlConnectionFactory("Server=localhost; Database=DemoDb; Trusted_Connection=True;Encrypt=false; MultipleActiveResultSets=true");
            var employeeId = 608;
            var c = new DapperEmployeeRepository(_Connection).Dapper_GetAllEmployeesAsync();

        }
    }
}
