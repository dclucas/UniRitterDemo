using System;
using System.Linq;
using UniRitter.Demo.DomainModel;
using FakeItEasy;
using System.Data.Entity;
using UniRitter.Demo.DataAccessLogic;
using NUnit.Framework;
using System.Data;
using Ploeh.AutoFixture;

namespace UniRitter.Demo.Tests.DataAccess
{
    [TestFixture(typeof(Autor))]
    [TestFixture(typeof(Livro))]
    [TestFixture(typeof(Genero))]
    public class RepositoryTest<TEntidade>
        where TEntidade : class, IEntidade, new()
    {
        [Test]
        public void Inserir_Redireciona()
        {
            var target = CriarTarget();
            var db = target.Context;
            var set = A.Fake<IDbSet<TEntidade>>();
            var entidade = new TEntidade();

            A.CallTo(() => db.BuscarTodos<TEntidade>())
                .Returns(set);

            target.Inserir(entidade);

            A.CallTo(() => db.BuscarTodos<TEntidade>())
                .MustHaveHappened();
            A.CallTo(() => set.Add(entidade))
                .MustHaveHappened();
            A.CallTo(() => db.SaveChanges()).MustHaveHappened();
        }

        [Test]
        public void Atualizar_Redireciona()
        {
            var target = CriarTarget();
            var db = target.Context;
            var set = A.Fake<IDbSet<TEntidade>>();
            var entidade = new TEntidade();

            target.Atualizar(entidade);

            A.CallTo(() => db.SetarEstado<TEntidade>(entidade, EntityState.Modified))
                .MustHaveHappened();

            A.CallTo(() => db.SaveChanges()).MustHaveHappened();
        }

        [Test]
        public void Remover_Redireciona()
        {
            var target = CriarTarget();
            var db = target.Context;
            var set = A.Fake<IDbSet<TEntidade>>();
            var entidade = new TEntidade();

            target.Remover(entidade);

            A.CallTo(() => db.SetarEstado<TEntidade>(
                entidade, 
                EntityState.Deleted))
                .MustHaveHappened();

            A.CallTo(() => db.SaveChanges()).MustHaveHappened();
        }

        [Test]
        public void BuscarPorId_Redireciona()
        {
            var target = CriarTarget();
            var db = target.Context;
            var entidade = new TEntidade();

            const int key = 152;
            A.CallTo(() => db.BuscarPorId<TEntidade>(key))
                .Returns(entidade);

            var res = target.BuscarPorId(key);

            A.CallTo(() => db.BuscarPorId<TEntidade>(key))
                .MustHaveHappened();

            Assert.AreEqual(entidade, res);
        }

        [Test]
        public void Buscar_ComInclusoes_Redireciona()
        {
            var target = CriarTarget();
            var db = target.Context;
            var inc = new string[] { "abc", "def" };
            var entidades = new TEntidade[5];
            A.CallTo(() => db.Buscar<TEntidade>(inc))
                .Returns(entidades);

            var res = target.Buscar(inc);

            A.CallTo(() => db.Buscar<TEntidade>(inc))
                .MustHaveHappened();
            Assert.AreEqual(entidades, res);
        }

        [Test]
        public void BuscarTodos_ComInclusoes_Redireciona()
        {
            var target = CriarTarget();
            var db = target.Context;
            var entidades = A.Fake<IDbSet<TEntidade>>();
            A.CallTo(() => db.BuscarTodos<TEntidade>())
                .Returns(entidades);

            var res = target.BuscarTodos();

            A.CallTo(() => db.BuscarTodos<TEntidade>())
                .MustHaveHappened();
            Assert.AreEqual(entidades, res);
        }

        [Test]
        public void Dispose_InvocaDispose()
        {
            var target = CriarTarget();

            target.Dispose();

            A.CallTo(() => target.Context.Dispose())
                .MustHaveHappened();
        }

        [Test]
        public void Atualizar_Nulo_LancaExcecao()
        {
            var target = CriarTarget();
            Assert.Throws<ArgumentNullException>(
                () => target.Atualizar(null));
        }

        [Test]
        public void Remover_Nulo_LancaExcecao()
        {
            var target = CriarTarget();
            Assert.Throws<ArgumentNullException>(
                () => target.Remover(null));
        }

        [Test]
        public void Inserir_Nulo_LancaExcecao()
        {
            var target = CriarTarget();
            Assert.Throws<ArgumentNullException>(
                () => target.Inserir(null));
        }

        [Test]
        public void BuscarPorNome_Nulo_LancaExcecao()
        {
            var target = CriarTarget();
            Assert.Throws<ArgumentNullException>(
                () => target.BuscarPorNome(null));
        }

        [Test]
        public void BuscarPorNome_Vazio_LancaExcecao()
        {
            var target = CriarTarget();
            Assert.Throws<ArgumentException>(
                () => target.BuscarPorNome(String.Empty));
        }

        internal virtual Repository<TEntidade> CriarTarget()
        {
            return new Repository<TEntidade>(
                A.Fake<IDataContext>());
        }

        [Test]
        public void BuscarPorNome_NomeExato_RetornaCorretamente()
        {
            var target = CriarTarget();
            var db = target.Context;
            var fixture = new Fixture();

            var entidades = fixture.CreateMany<TEntidade>().ToArray();
            var entidade = entidades.OrderBy(e => e.Nome).First();
            var nome = entidade.Nome;
            var esperado = entidades.Where(e => e.Nome.Contains(nome)).ToArray();

            A.CallTo(() => db.Buscar<TEntidade>())
                .Returns(entidades);

            var res = target.BuscarPorNome(nome).ToArray();

            A.CallTo(() => db.Buscar<TEntidade>())
                .MustHaveHappened();

            CollectionAssert.AreEquivalent(esperado, res);
        }
    }
}
