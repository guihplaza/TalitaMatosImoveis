using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T> where T : class
    {
        IList<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        T GetById(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
