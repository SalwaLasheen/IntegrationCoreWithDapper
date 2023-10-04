using BenchmarkDotNet.Attributes;
using EFCoreVsDapper.Models;
using EFCoreVsDapper.Repos;

namespace EFCore2VSDapper
{
    public class EFCore2VsDapper
    {
        EFRepository _EFRepo;
        DapperRepository _DapperRepo;
        CoreAsDapperRepository _coreAsDapperRepository;

        public EFCore2VsDapper()
        {
            _EFRepo = new EFRepository();
            _DapperRepo = new DapperRepository();
            _coreAsDapperRepository = new CoreAsDapperRepository();

        }

        [Benchmark]
        public List<Employee> GetEmployeesWithEntityFramework() => _EFRepo.GetEmployeesWithEF();

        [Benchmark]
        public List<Employee> GetEmployeesWithDapper() => _DapperRepo.GetEmployeesWithDapper();

        [Benchmark]
        public List<Employee> GetEmployeesCoreAsDapper() => _coreAsDapperRepository.GetEmployeesWithEFAsDapper();
    }
}
