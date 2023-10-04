using System.Data;

namespace DataAccessLayer.Application.Repositories.Shared.Interfaces
{
    public interface IApplicationReadQuery
    {
        Task<IReadOnlyList<T>> QueryAsync<T>(string sqlQuery, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
        Task<T> QueryFirstOrDefaultAsync<T>(string sqlQuery, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
    }
}
