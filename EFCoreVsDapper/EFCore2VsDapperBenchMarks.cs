using BenchmarkDotNet.Attributes;
using EFCoreVsDapper.Models;
using EFCoreVsDapper.Repos;

namespace EFCore2VSDapper
{
    public class EFCore2VsDapperBenchMarks
    {
        EFRepository _EFRepo;
        DapperRepository _DapperRepo;
        CoreAsDapperRepository _coreAsDapperRepository;

        public EFCore2VsDapperBenchMarks()
        {
            _EFRepo = new EFRepository();
            _DapperRepo = new DapperRepository();
            _coreAsDapperRepository = new CoreAsDapperRepository();

        }

        [Benchmark]
        public List<string> GetEmployeesNameWithDapper() => _DapperRepo.GetEmployeesWithDapper();
        [Benchmark]
        public List<string> GetEmployeesNameCoreAsDapperSQlQueryRow() => _coreAsDapperRepository.GetEmployeesWithEFAsDapperSQLQueryRow();

        [Benchmark]
        public List<string> GetEmployeesNameWithEntityFramework() => _EFRepo.GetEmployeesNameWithEFCoreWithNoTracking();

        [Benchmark]
        public List<string> GetEmployeesNameCoreAsDapperSQlQuery() => _coreAsDapperRepository.GetEmployeesWithEFAsDapperSQLQuery();
        [Benchmark]
        public List<string> GetEmployeesNameCoreAsDapperSQlRow() => _coreAsDapperRepository.GetEmployeesWithEFAsDapperFromSqLROW();
       
        [Benchmark]
        public List<string> GetEmployeesNameCoreAsDapperFromSql() => _coreAsDapperRepository.GetEmployeesWithEFAsDapperFromSql();

    }
}
