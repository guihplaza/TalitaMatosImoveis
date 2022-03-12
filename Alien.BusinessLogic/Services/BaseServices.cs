using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using Microsoft.Extensions.Configuration;
using Repositorio;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using BusinessLogic.Services;
using System.Threading.Tasks;
using X.PagedList;

namespace BusinessLogic
{
    public class BaseServices<T> : IBaseServices<T> where T : class //: IDisposable
    {
        IBaseRepository<T> _repository;

        public BaseServices(ConexaoBanco conexaoBanco)
        {
            _repository = new BaseRepository<T>(conexaoBanco);
        }

        public async Task<IList<T>> Listar()
        {
            var x = await _repository.Get();
            return x;
        }
        public async Task<IList<T>> Listar(Expression<Func<T, bool>> predicate, int pageIndex = 1, int pageSize = 999999999)
        {
            var x = await _repository.Get(predicate, pageIndex, pageSize);
            return x;
        }

        public async Task<IPagedList<T>> ListarPagedList(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
        {
            var x = await _repository.GetPagedList(predicate, pageNumber, pageSize);
            return x;
        }        
        
        public async Task<IList<T>> Listar(Expression<Func<T, bool>> predicate)
        {
            var x = await _repository.GetWithoutLimitPages(predicate);
            return x;
        }

        public async Task Adicionar(T obj)
        {
            await _repository.Add(obj);
        }
        public async Task Adicionar(IEnumerable<T> obj)
        {
            await _repository.Add(obj);
        }
        public async Task Excluir(T obj)
        {
            await _repository.Delete(obj);
        }
        public async Task Excluir(IEnumerable<T> obj)
        {
            await _repository.Delete(obj);
        }

        public async Task Alterar(T obj)
        {
            await _repository.Update(obj);
        }
        public async Task Alterar(IEnumerable<T> obj)
        {
            await _repository.Update(obj);
        }
        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _repository.GetById(predicate);
        }        
        
        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await _repository.Count(predicate);
        }
    }
}
