using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRitter.Demo.DomainModel;
using UniRitter.Demo.DataAccessLogic;
using Seterlund.CodeGuard;

namespace UniRitter.Demo.BusinessLogic
{
    internal class BusinessObject<TEntidade> 
        : IBusinessObject<TEntidade> 
        where TEntidade : class, IEntidade
    {
        public BusinessObject(IRepository<TEntidade> repo)
        {
            this.Repo = repo;
        }

        public void Inserir(TEntidade entidade)
        {
            Guard.That(() => entidade).IsNotNull();
            Repo.Inserir(entidade);
        }


        public void Remover(TEntidade entidade)
        {
            Guard.That(() => entidade).IsNotNull();
            Repo.Remover(entidade);
        }

        public void Atualizar(TEntidade entidade)
        {
            Guard.That(() => entidade).IsNotNull();
            throw new NotImplementedException();
        }

        public TEntidade BuscarPorId(int id)
        {
            return Repo.BuscarPorId(id);
        }

        public IEnumerable<TEntidade> BuscarPorNome(string nome)
        {
            Guard.That(() => nome).IsNotNull().IsNotEmpty();
            return Repo.BuscarPorNome(nome);
        }

        public IEnumerable<TEntidade> BuscarTodos()
        {
            return Repo.BuscarTodos();
        }

        public IRepository<TEntidade> Repo { get; private set; }
    }
}
