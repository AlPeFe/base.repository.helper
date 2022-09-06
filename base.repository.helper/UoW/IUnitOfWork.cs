using repository.helper.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace repository.helper.UoW
{
    public interface IUnitOfWork
    {
        int Save();
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
        void Dispose(bool disposing);
        IWriteRepository<T> GetRepository<T>() where T : class;
        IReadRepository<T> GetReadRepository<T>() where T : class;
    }
}
