namespace DataAccessLayer.Application.Repositories.Interface
{
    public interface IDapperRepository<T>
    {
        Task<List<T>> Dapper_GetAllEmployeesAsync();
        Task<T> Dapper_GetEmployeeByIdAsync(int id);
        Task<int> Dapper_CreateNewEmployeeAsync(T model);
        Task<int> Dapper_UpdateEmployeeAsync(T model, int id);
        Task<int> Dapper_DeleteEmployeeAsync(int id);

    }
}
