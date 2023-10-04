using System.Data;
namespace DataAccessLayer.Application.Repositories.Shared.Interfaces
{
    public interface IApplicationWriteQuery
    {
        Task<int> ExecuteAsync(string sqlQuery, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
    }
}

