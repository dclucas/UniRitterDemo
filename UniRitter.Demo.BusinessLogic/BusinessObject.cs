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

        public virtual void Inserir(TEntidade entidade)
        {
            Guard.That(() => entidade).IsNotNull();
            Repo.Inserir(entidade);
        }


        public virtual void Remover(TEntidade entidade)
        {
            Guard.That(() => entidade).IsNotNull();
            Repo.Remover(entidade);
        }

        public virtual void Atualizar(TEntidade entidade)
        {
            Guard.That(() => entidade).IsNotNull();
            throw new NotImplementedException();
        }

        public virtual TEntidade BuscarPorId(int id)
        {
            return Repo.BuscarPorId(id);
        }

        public virtual IEnumerable<TEntidade> BuscarPorNome(string nome)
        {
            Guard.That(() => nome).IsNotNull().IsNotEmpty();
            return Repo.BuscarPorNome(nome);
        }

        public virtual IEnumerable<TEntidade> BuscarTodos()
        {
            return Repo.BuscarTodos();
        }

        public IRepository<TEntidade> Repo { get; private set; }
    }
}
