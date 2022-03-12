using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Repositorio
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        ConexaoBanco _context;
        public BaseRepository(ConexaoBanco context)
        {
            _context = context;
        }

        public async Task Add(T item)
        {
            _context.Set<T>().Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Add(IEnumerable<T> item)
        {
            _context.Set<T>().AddRange(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T item)
        {
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(IEnumerable<T> item)
        {
            _context.Set<T>().RemoveRange(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> Get()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Update(T item)
        {
            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(IEnumerable<T> item)
        {
            _context.Set<T>().UpdateRange(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> Get(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize)
        {
            int skip = (pageIndex - 1) * pageSize;

            return await _context.Set<T>().Where(predicate).Skip<T>(skip).Take<T>(pageSize).ToListAsync();
        }

        public async Task<IList<T>> GetWithoutLimitPages(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).Distinct().ToListAsync();
        }

        public async Task<IPagedList<T>> GetPagedList(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
        {
            return await _context.Set<T>().Where(predicate).Distinct().ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task Dispose()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).CountAsync();
        }

        void IDisposable.Dispose()
        {
            _context.DisposeAsync();
        }
    }
}
