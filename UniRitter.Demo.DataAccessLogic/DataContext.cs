using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using UniRitter.Demo.DomainModel;

namespace UniRitter.Demo.DataAccessLogic
{
    internal class DataContext : DbContext, UniRitter.Demo.DataAccessLogic.IDataContext
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
    }
}
