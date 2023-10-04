using Dapper;
using DataAccessLayer.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Application.Repositories.Shared.Implementations
{
    public class ApplicationWriteQuery
    {
        private readonly IApplicationDbContext context;
        public ApplicationWriteQuery(IApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await context.Connection.ExecuteAsync(sql, param, transaction);
        }
        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return (await context.Connection.QueryAsync<T>(sql, param, transaction)).AsList();
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await context.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }
       
    }
}
