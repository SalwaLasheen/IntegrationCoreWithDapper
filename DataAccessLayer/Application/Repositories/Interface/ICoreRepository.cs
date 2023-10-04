namespace DataAccessLayer.Application.Repositories.Interface
{
    public interface ICoreRepository<T>
    {
        Task<List<T>> Core_GetAllEmployeesAsync();
        Task<T> Core_GetEmployeeByIdAsync(int id);
        Task Core_CreateNewEmployeeAsync(T model);
        Task Core_UpdateEmployeeAsync(T model, int id);
        Task Core_DeleteEmployeeAsync(int id);
    }
}
