using BuildingCharge.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.Interfaces
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<T?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<List<T>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default);
        Task<List<T>> GetAllAsync(CancellationToken ct = default);
        Task<T> AddAsync(T entity, CancellationToken ct = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);
        Task UpdateAsync(T entity, CancellationToken ct = default);
        Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);
        Task DeleteAsync(T entity, CancellationToken ct = default);
        Task<int> SaveChangesAsync(CancellationToken ct = default);

        Task<(List<T> Items, int TotalCount)> GetPagedAsync(
                   int pageNumber,
                   int pageSize,
                   Expression<Func<T, bool>>? filter = null,
                   Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                   CancellationToken ct = default
               );
    }
}
