using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRitter.Demo.DomainModel;
using UniRitter.Demo.DataAccessLogic;

namespace UniRitter.Demo.BusinessLogic
{
    internal abstract class BusinessObject<TEntidade> 
        : IBusinessObject<TEntidade> 
        where TEntidade : class, IEntidade
    {
        public BusinessObject(IRepository<TEntidade> repo)
        {
            this.Repo = repo;
        }

        public void Inserir(TEntidade entidade)
        {
            throw new NotImplementedException();
        }


        public void Remover(TEntidade entidade)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(TEntidade entidade)
        {
            throw new NotImplementedException();
        }

        public TEntidade BuscaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntidade> BuscaPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntidade> BuscaTodos()
        {
            throw new NotImplementedException();
        }

        public IRepository<TEntidade> Repo { get; private set; }
    }
}
