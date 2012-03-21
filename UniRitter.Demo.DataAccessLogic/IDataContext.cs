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
        int SaveChanges();

        IDbSet<T> BuscarTodos<T>()
            where T : class;

        IEnumerable<T> Buscar<T>(params string[] inclusoes)
            where T : class;

        void SetarEstado<T>(T entidade, EntityState state) 
            where T : class;

        T BuscarPorId<T>(int id)
            where T : class;
    }
}
