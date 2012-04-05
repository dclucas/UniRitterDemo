namespace UniRitter.Demo.BusinessLogic
{
    using System.Collections.Generic;
    using System.Linq;
    using UniRitter.Demo.DataAccessLogic;
    using UniRitter.Demo.DomainModel;

    internal class LivroBO : BusinessObject<Livro>
    {
        public LivroBO(IRepository<Livro> repo)
            : base(repo)
        {
        }

        public override IEnumerable<Livro> BuscarTodos()
        {
            return Repo.Buscar("Autor", "Genero");
        }

        public override Livro BuscarPorId(int id)
        {
            return Repo.Buscar("Autor", "Genero")
                .SingleOrDefault(l => l.Id == id);
        }
    }
}
