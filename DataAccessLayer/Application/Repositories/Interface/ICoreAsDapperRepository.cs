namespace DataAccessLayer.Application.Repositories.Interface
{
    public interface ICoreAsDapperRepository<T>
    {
        Task<List<T>> CoreAsDapper_GetAllEmployeesAsync();
        Task<T> CoreAsDapper_GetEmployeeByIdAsync(int id);
        Task CoreAsDapper_CreateNewEmployeeAsync(T model);
        Task CoreAsDapper_UpdateEmployeeAsync(T model, int id);
        Task CoreAsDapper_DeleteEmployeeAsync(int id);

    }
}
