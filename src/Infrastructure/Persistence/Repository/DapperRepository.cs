using System.Data;
using Dapper;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TD.WebApi.Application.Common.Models;
using TD.WebApi.Application.Common.Persistence;
using TD.WebApi.Domain.Common.Contracts;
using TD.WebApi.Infrastructure.Persistence.Context;

namespace TD.WebApi.Infrastructure.Persistence.Repository;

public class DapperRepository : IDapperRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DapperRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    where T : class, IEntity =>
        (await _dbContext.Connection.QueryAsync<T>(sql, param, transaction))
            .AsList();

    public async Task<IReadOnlyList<T>> QueryAsyncNew<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
   where T : class
    {
        sql = sql.Replace("@tenant", _dbContext.TenantInfo.Id);
        return (await _dbContext.Connection.QueryAsync<T>(sql, param, transaction)).AsList();
    }


    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    where T : class, IEntity
    {
        if (_dbContext.Model.GetMultiTenantEntityTypes().Any(t => t.ClrType == typeof(T)))
        {
            sql = sql.Replace("@tenant", _dbContext.TenantInfo.Id);
        }

        return await _dbContext.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }

    public Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    where T : class, IEntity
    {
        if (_dbContext.Model.GetMultiTenantEntityTypes().Any(t => t.ClrType == typeof(T)))
        {
            sql = sql.Replace("@tenant", _dbContext.TenantInfo.Id);
        }

        return _dbContext.Connection.QuerySingleAsync<T>(sql, param, transaction);
    }


    public async Task<T?> QueryFirstOrDefaultObjectAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
  where T : class
    {
        sql = sql.Replace("@tenant", _dbContext.TenantInfo.Id);
        return await _dbContext.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }


    public async Task<PaginationResponse<T>> PaginatedListAsync<T>(string sql, int page, int pageSize, CancellationToken cancellationToken = default)
            where T : class
    {
        sql = sql.Replace("@tenant", _dbContext.TenantInfo.Id);
        var results = new PaginationResponse<T>();
        results.PageSize = pageSize;
        results.CurrentPage = page;

        using (var multi = await _dbContext.Connection.QueryMultipleAsync(sql))
        {
            results.Data = (await multi.ReadAsync<T>()).ToList();
            int count = multi.ReadFirst<int>();
            results.TotalCount = count;
            results.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        return results;
    }

    public async Task<PaginationResponse<T>> PaginatedListNewAsync<T>(string sql, int page, int pageSize, CancellationToken cancellationToken = default)
            where T : class
    {
        sql = sql.Replace("@tenant", _dbContext.TenantInfo.Id);
        var results = new PaginationResponse<T>();
        results.PageSize = pageSize;
        results.CurrentPage = page;


        var lst = (await _dbContext.Connection.QueryAsync<T>(sql)).ToList();
        int count = 0;
        try
        {
            if (lst.Count > 0)
            {
                JObject jobj = JObject.Parse(JsonConvert.SerializeObject(lst.First()));
                count = int.Parse(jobj["TotalCount"].ToString());
            }
        }
        catch (Exception ex)
        {

        }
        results.Data = lst;
        results.TotalCount = count;
        results.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        return results;
    }

    public async Task<int?> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        sql = sql.Replace("@tenant", _dbContext.TenantInfo.Id);
        return await _dbContext.Connection.ExecuteAsync(sql, param, transaction);
    }

    public async Task<T?> ExecuteScalarAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        sql = sql.Replace("@tenant", _dbContext.TenantInfo.Id);
        return await _dbContext.Connection.ExecuteScalarAsync<T>(sql, param, transaction);
    }
}