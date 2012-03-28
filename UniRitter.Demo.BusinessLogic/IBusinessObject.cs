using System;
using UniRitter.Demo.DomainModel;
using System.Collections.Generic;
namespace UniRitter.Demo.BusinessLogic
{
    public interface IBusinessObject<TEntidade>
     where TEntidade : IEntidade
    {
        void Inserir(TEntidade entidade);

        void Remover(TEntidade entidade);

        void Atualizar(TEntidade entidade);

        TEntidade BuscarPorId(int id);

        IEnumerable<TEntidade> BuscarPorNome(string nome);

        IEnumerable<TEntidade> BuscarTodos();
    }
}
