// -----------------------------------------------------------------------
// <copyright file="LookupHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UniRitter.Demo.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using UniRitter.Demo.DataAccessLogic;
    using UniRitter.Demo.DomainModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class LookupHelper : ILookupHelper
    {
        internal Func<Type, object> CreateFunc { get; private set; }

        public LookupHelper(Func<Type, object> createFunc)
        {
            CreateFunc = createFunc;
        }

        public IEnumerable<T> BuscarTodos<T>()
            where T : class, IEntidade
        {
            return GetRepo<T>().BuscarTodos().OrderBy(e => e.Nome);
        }

        public IEnumerable<T> BuscarPorNome<T>(string nome)
            where T : class, IEntidade
        {
            return GetRepo<T>().BuscarPorNome(nome).OrderBy(e => e.Nome);
        }

        private IRepository<T> GetRepo<T>()
            where T : class, IEntidade
        {
            return (IRepository<T>)CreateFunc(typeof(IRepository<T>));
        }
    }
}
