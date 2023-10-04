using BenchmarkDotNet.Attributes;
using EFCoreVsDapper.Repos;

namespace EFCore2VSDapper
{
    public class EFCorePerformanceBenchMarks
    {
        EFRepository _EFRepo;
        CoreAsDapperRepository _coreAsDapperRepository;

        public EFCorePerformanceBenchMarks()
        {
            _EFRepo = new EFRepository();
            _coreAsDapperRepository = new CoreAsDapperRepository();

        }

        [Benchmark]
        public List<string> GetEmployeesWithEntityFrameworkWithNoTracking() => _EFRepo.GetEmployeesNameWithEFCoreWithNoTracking();

        [Benchmark]
        public List<string> GetEmployeesCoreAsDapperSQLQuery() => _coreAsDapperRepository.GetEmployeesWithEFAsDapperSQLQuery();
        [Benchmark]
        public List<string> GetEmployeesCoreAsDapperSQLQueryRow() => _coreAsDapperRepository.GetEmployeesWithEFAsDapperSQLQueryRow();
        [Benchmark]
        public List<string> GetEmployeesCoreAsDapperFromSqlRow() => _coreAsDapperRepository.GetEmployeesWithEFAsDapperFromSqLROW();
        [Benchmark]
        public List<string> GetEmployeesCoreAsDapperFromSql() => _coreAsDapperRepository.GetEmployeesWithEFAsDapperFromSql();
    }
}
