namespace DataAccessLayer.Application.Repositories.Interface
{
    public interface ICompiledRepository<T>
    {
        List<T> Compiled_GetAllEmployeesAsync();
        Task<T> Compiled_GetEmployeeByIdAsync(int id);

    }
}
