using System;
using System.Collections.Generic;
using System.Data.Entity;
namespace UniRitter.Demo.DataAccessLogic
{
    public interface IDataContext : IDisposable
    {
        IDbSet<T> BuscarTodos<T>() where T : class;

        int SaveChanges();
    }
}
