using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace repository.helper.Contracts
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity>, IDisposable
      where TEntity : class
    {
        protected readonly DbContext _context;

        public ReadRepository(DbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Dynamic Linq
        /// </summary>
        /// <param name="predicate">Dynamic Linq predicate</param>
        /// <param name="trackingState">sets tracking option</param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll(string? predicate = null, TrackingState trackingState = TrackingState.Disabled)
        {
            var query = _context
                    .Set<TEntity>()
                    .AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            if (trackingState is TrackingState.Enabled)
                return query;

            return query.AsNoTracking();
        }
        /// <summary>
        /// Sql expression filtering
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <param name="trackingState">sets tracking option </param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null, TrackingState trackingState = TrackingState.Disabled)
        {
            var query = _context
                    .Set<TEntity>()
                    .AsQueryable();

            if (expression != null)
                query = query.Where(expression);

            if (trackingState is TrackingState.Enabled)
                return query;

            return query.AsNoTracking();
        }

        public Task<TEntity> FindById(Guid id, CancellationToken cancellationToken = default)
            => _context.Set<TEntity>().FindAsync(id, cancellationToken)
            .AsTask();

        public PagedResult<TEntity> GetAllPaged(int page, int pageSize, string? predicate = null, TrackingState trackingState = TrackingState.Disabled)
        {
            var result = GetAll(predicate, trackingState);
            return result.PageResult(page, pageSize);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public enum TrackingState
    {
        Enabled, Disabled
    }
}
