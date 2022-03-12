using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Repositorio
{
    public interface IBaseRepository<T> where T : class
    {
        Task Add(T item);

        Task Add(IEnumerable<T> item);

        Task Delete(T item);
        Task Delete(IEnumerable<T> item);

        Task<IList<T>> Get();

        Task Update(T item);
        Task Update(IEnumerable<T> item);

        Task<IList<T>> Get(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize);

        Task<IList<T>> GetWithoutLimitPages(Expression<Func<T, bool>> predicate);

        Task<IPagedList<T>> GetPagedList(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);

        Task<T> GetById(Expression<Func<T, bool>> predicate);

        Task<int> Count(Expression<Func<T, bool>> predicate);
    }
}