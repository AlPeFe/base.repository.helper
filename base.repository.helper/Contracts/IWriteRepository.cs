using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace repository.helper.Contracts
{
    public interface IWriteRepository<T> where T : class
    {
        void Update(T entity);
        void Delete(T entity);
        void Add(T entity);
        Task AddEntityAsync(T entity);
    }
}
