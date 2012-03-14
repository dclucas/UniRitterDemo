using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRitter.Demo.DomainModel;
using System.Linq.Expressions;

namespace UniRitter.Demo.DataAccessLogic
{
    public interface IRepository<T>
        where T : class, IEntidade
    {
        void Inserir(T entidade);

        void Remover(T entidade);

        void Atualizar(T entidade);

        T BuscarPorId(int it);

        IEnumerable<T> BuscarPorNome(string nome);

        IEnumerable<T> BuscarTodos();

        IEnumerable<T> Buscar(Expression<Func<T, bool>> func);
    }
}
