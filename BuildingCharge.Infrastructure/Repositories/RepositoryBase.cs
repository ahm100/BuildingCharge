using BuildingCharge.Core.Application.Interfaces;
using BuildingCharge.Core.Domain.Common;
using BuildingCharge.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<T> _set;

        public RepositoryBase(AppDbContext db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
              => await _set.FindAsync(new object[] { id }, ct);

        public virtual async Task<List<T>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
            => await _set
                .Where(e => ids.Contains(e.Id))
                .AsNoTracking()
                .ToListAsync(ct);


        public virtual async Task<List<T>> GetAllAsync(CancellationToken ct = default)
            => await _set.AsNoTracking().ToListAsync(ct);

        public virtual async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            entity.CreatedDate = DateTime.Now;
            await _set.AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);
            return entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
        {
            var now = DateTime.Now;
            foreach (var e in entities) e.CreatedDate = now;
            await _set.AddRangeAsync(entities, ct);
            await _db.SaveChangesAsync(ct);
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            entity.UpdatedDate = DateTime.Now;
            _set.Update(entity);
            await _db.SaveChangesAsync(ct);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
        {
            var now = DateTime.Now;
            foreach (var e in entities) e.UpdatedDate = now;
            _set.UpdateRange(entities);
            await _db.SaveChangesAsync(ct);
        }

        public virtual async Task DeleteAsync(T entity, CancellationToken ct = default)
        {
            _set.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
            => _db.SaveChangesAsync(ct);


        public async Task<(List<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber, int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            CancellationToken ct = default)
        {
            IQueryable<T> query = _db.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            var totalCount = await query.CountAsync(ct);

            if (orderBy != null)
                query = orderBy(query);
            else
                query = query.OrderBy(e => e.Id); // پیش‌فرض بر اساس Id

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(ct);

            return (items, totalCount);
        }

    }
}
