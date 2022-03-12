using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using Microsoft.Extensions.Configuration;
using Repositorio;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Threading.Tasks;
using X.PagedList;

namespace BusinessLogic
{
    public interface IBaseServices<T> where T : class
    {
        Task Adicionar(T obj);

        Task Adicionar(IEnumerable<T> obj);

        Task Excluir(T obj);
        Task Excluir(IEnumerable<T> obj);

        Task Alterar(T obj);
        Task Alterar(IEnumerable<T> obj);

        Task<T> GetById(Expression<Func<T, bool>> predicate);

        Task<IList<T>> Listar();

        Task<IList<T>> Listar(Expression<Func<T, bool>> predicate, int pageIndex = 1, int pageSize = 999999999);

        Task<IList<T>> Listar(Expression<Func<T, bool>> predicate);

        Task<IPagedList<T>> ListarPagedList(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);

        Task<int> Count(Expression<Func<T, bool>> predicate);
    }
}
