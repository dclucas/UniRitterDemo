using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRitter.Demo.DomainModel;
using System.Data.Entity;
using System.Data;

namespace UniRitter.Demo.DataAccessLogic
{
    internal class Repository<T> : IRepository<T>, IDisposable
        where T : class, IEntidade
    {
        public Repository(IDataContext context)
        {
            Context = context;
        }

        protected IDataContext Context { get; private set; }

        public virtual void Inserir(T entidade)
        {
            var set = Context.BuscarTodos<T>();
            set.Add(entidade);
            Context.SaveChanges();
        }

        public virtual void Remover(T entidade)
        {
            SalvarEstado(entidade, EntityState.Deleted);
        }

        protected void SalvarEstado(T entidade, EntityState entityState)
        {
            Context.SetarEstado<T>(entidade, entityState);
            Context.SaveChanges();
        }

        public virtual void Atualizar(T entidade)
        {
            SalvarEstado(entidade, EntityState.Modified);
        }

        public virtual T BuscarPorId(int id)
        {
            return Context.BuscarPorId<T>(id);
        }

        public virtual IEnumerable<T> BuscarPorNome(string nome)
        {
            return Context.Buscar<T>(o => o.Nome.Contains(nome));
        }

        public virtual IEnumerable<T> BuscarTodos()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> Buscar(System.Linq.Expressions.Expression<Func<T, bool>> func)
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
