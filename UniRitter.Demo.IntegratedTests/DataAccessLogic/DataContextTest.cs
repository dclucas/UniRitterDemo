using System.Data;
using Ploeh.AutoFixture;

namespace UniRitter.Demo.IntegratedTests.DataAccessLogic
{
    using System.Linq;
    using NUnit.Framework;
    using UniRitter.Demo.DataAccessLogic;
    using UniRitter.Demo.DomainModel;
    using System.Diagnostics;

    //[Ignore]
    [TestFixture(typeof(Autor))]
    [TestFixture(typeof(Genero))]
    [TestFixture(typeof(Livro))]
    public class DataContextTest<TEntidade> where TEntidade : class, IEntidade, new()
    {
        [Test]
        public void TesteIntegrado()
        {
            var e = CriarEntidade();
            Add_InsereEmBanco(e);
            Atualizar_AlteraEmBanco(e);
            Deletar_RemoveDoBanco(e);
        }

        public void Add_InsereEmBanco(TEntidade entidade)
        {
            var target = new DataContext();
            target.Set<TEntidade>().Add(entidade);
            target.SaveChanges();
            Debug.WriteLine("Entidade criada no banco. {0}", ListarEntidade(entidade));

            VerificarBuscas(entidade);
        }

        public void Atualizar_AlteraEmBanco(TEntidade entidade)
        {
            entidade.Nome += "(upd)";
            var target = new DataContext();
            target.SetarEstado(entidade, EntityState.Modified);
            target.SaveChanges();
            Debug.WriteLine("Entidade atualizada no banco. {0}", ListarEntidade(entidade));

            VerificarBuscas(entidade);
        }

        public void Deletar_RemoveDoBanco(TEntidade entidade)
        {
            var target = new DataContext();
            target.SetarEstado(entidade, EntityState.Deleted);
            target.SaveChanges();
            Debug.WriteLine("Entidade removida do banco. {0}", ListarEntidade(entidade));

            VerificarAusencia(entidade);
        }

        private void VerificarAusencia(TEntidade entidade)
        {
            var contexto = new DataContext();
            var res = contexto.BuscarPorId<TEntidade>(entidade.Id);
            Assert.Null(res);
            Debug.WriteLine("Entidade não encontrada por Id (como esperado). {0}", ListarEntidade(entidade));
            res = contexto.Buscar<TEntidade>().SingleOrDefault(o => entidade.Nome.Equals(o.Nome));
            Assert.Null(res);
            Debug.WriteLine("Entidade não encontrada por Nome (como esperado). {0}", ListarEntidade(entidade));
        }

        public TEntidade CriarEntidade()
        {
            var fixture = new Fixture();
            var entidade = fixture.CreateAnonymous<TEntidade>();
            return entidade;
        }

        protected void VerificarBuscas(TEntidade entidade)
        {
            var contexto = new DataContext();
            var res = contexto.BuscarPorId<TEntidade>(entidade.Id);
            VerificarIgualdade(entidade, res);
            Debug.WriteLine("Entidade encontrada por Id. {0}", ListarEntidade(entidade));
            res = contexto.Buscar<TEntidade>().Single(o => entidade.Nome.Equals(o.Nome));
            VerificarIgualdade(entidade, res);
            Debug.WriteLine("Entidade encontrada por Nome. {0}", ListarEntidade(entidade));
        }

        protected virtual string ListarEntidade(TEntidade e)
        {
            return string.Format("Tipo: {2}, Id: {0}, Nome: {1}", e.Id, e.Nome, e.GetType().Name);
        }

        private void VerificarIgualdade(TEntidade esperado, TEntidade resultado)
        {
            Assert.AreEqual(esperado.Id, resultado.Id);
            Assert.AreEqual(esperado.Nome, resultado.Nome);
        }
    }
}
