using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace repository.helper.Contracts
{
    public interface IReadRepository<TEntity> where TEntity
     : class
    {
        IQueryable<TEntity> GetAll(string? predicate = null, TrackingState trackingState = TrackingState.Disabled);
        Task<TEntity> FindById(Guid id, CancellationToken cancellationToken = default);
        PagedResult<TEntity> GetAllPaged(int page, int pageSize, string? predicate = null, TrackingState trackingState = TrackingState.Disabled);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null, TrackingState trackingState = TrackingState.Disabled);


    }
}
