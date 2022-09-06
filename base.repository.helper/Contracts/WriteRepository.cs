using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace repository.helper.Contracts
{
    public class WriteRepository<TEntity> : ReadRepository<TEntity>, IWriteRepository<TEntity>
     where TEntity : class
    {
        public WriteRepository(DbContext context)
            : base(context)
        {

        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public async Task AddEntityAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity is null || _context.Entry(entity).State != EntityState.Detached)
                return;

            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }


    }
}
