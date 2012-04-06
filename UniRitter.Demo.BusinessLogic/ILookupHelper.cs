namespace UniRitter.Demo.BusinessLogic
{
    using System.Collections.Generic;

    using UniRitter.Demo.DomainModel;

    public interface ILookupHelper
    {
        IEnumerable<T> BuscarTodos<T>()
            where T : class, IEntidade;

        IEnumerable<T> BuscarPorNome<T>(string nome)
            where T : class, IEntidade;
    }
}