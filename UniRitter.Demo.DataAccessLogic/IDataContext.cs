using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Linq.Expressions;
namespace UniRitter.Demo.DataAccessLogic
{
    public interface IDataContext : IDisposable
    {
        IDbSet<T> BuscarTodos<T>() where T : class;

        int SaveChanges();

        void SetarEstado<T>(T entidade, EntityState state) where T : class;

        T BuscarPorId<T>(int id) where T : class;

        IEnumerable<T> Buscar<T>(Expression<Func<T, bool>> predicado)
            where T : class;
    }
}
