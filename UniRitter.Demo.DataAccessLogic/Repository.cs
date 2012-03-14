using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRitter.Demo.DomainModel;
using System.Data.Entity;

namespace UniRitter.Demo.DataAccessLogic
{
    internal class Repository<T> : IRepository<T>, IDisposable
        where T : class, IEntidade
    {
        public Repository(IDataContext context)
        {
            Context = context;
        }

        public IDataContext Context { get; private set; }

        public void Inserir(T entidade)
        {
            var set = Context.BuscarTodos<T>();
            set.Add(entidade);
            Context.SaveChanges();
        }

        public void Remover(T entidade)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(T entidade)
        {
            throw new NotImplementedException();
        }

        public T BuscarPorId(int it)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> BuscarPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> BuscarTodos()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Buscar(System.Linq.Expressions.Expression<Func<T, bool>> func)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
