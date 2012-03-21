using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using UniRitter.Demo.DomainModel;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Linq.Expressions;

namespace UniRitter.Demo.DataAccessLogic
{
    internal class DataContext : DbContext, IDataContext
    {
        public virtual DbSet<Autor> Autores { get; set; }
        
        public virtual DbSet<Livro> Livros { get; set; }

        public virtual DbSet<Genero> Generos { get; set; }

        public DataContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
        }

        public IDbSet<T> BuscarTodos<T>()
            where T : class
        {
            return this.Set<T>();
        }

        public IEnumerable<T> Buscar<T>(Expression<Func<T, bool>> predicado)
            where T : class
        {
            return this.Set<T>().Where(predicado);
        }

        public IEnumerable<T> Buscar<T>()
            where T : class
        {
            return this.Set<T>();
        }

        public IEnumerable<T> Buscar<T>(params string[] inclusoes)
            where T : class
        {
            return BuscarComInclusoes<T>(this.Set<T>(), inclusoes);
        }

        internal IEnumerable<T> BuscarComInclusoes<T>(
            IQueryable<T> query,
            params string[] inclusoes)
            where T : class
        {
            return inclusoes.Aggregate(query, (current, i) => current.Include(i));
        }

        public void SetarEstado<T>(T entidade, EntityState state) 
            where T : class
        {
            this.Entry<T>(entidade).State = state;
        }

        public T BuscarPorId<T>(int id)
            where T : class
        {
            return this.Set<T>().Find(id);
        }
    }
}
