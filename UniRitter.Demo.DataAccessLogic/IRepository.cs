using System;
using UniRitter.Demo.DomainModel;
using System.Collections.Generic;
namespace UniRitter.Demo.DataAccessLogic
{
    public interface IRepository<TEntidade>
     where TEntidade : IEntidade
    {
        void Inserir(TEntidade entidade);

        void Remover(TEntidade entidade);

        void Atualizar(TEntidade entidade);

        TEntidade BuscaPorId(int id);

        IEnumerable<TEntidade> BuscaPorNome(string nome);

        IEnumerable<TEntidade> BuscaTodos();
    }
}
