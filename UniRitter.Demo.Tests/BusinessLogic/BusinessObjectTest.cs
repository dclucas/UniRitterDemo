using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UniRitter.Demo.BusinessLogic;
using UniRitter.Demo.DomainModel;
using UniRitter.Demo.DataAccessLogic;
using FakeItEasy;
using Ploeh.AutoFixture;

namespace UniRitter.Demo.Tests.BusinessLogic
{
    [TestFixture(typeof(Autor))]
    [TestFixture(typeof(Genero))]
    [TestFixture(typeof(Livro))]
    public class BusinessObjectTest<T>
        where T : class, IEntidade, new()
    {
        [Test]
        public void Inserir_RedirecionaRepo()
        {
            var fixture = new Fixture();
            var entidade = fixture.CreateAnonymous<T>();
            var target = CriarTarget();
            var repo = target.Repo;

            target.Inserir(entidade);

            A.CallTo(() => repo.Inserir(entidade))
                .MustHaveHappened();
        }

        [Test]
        public void BuscarPorNome_RedirecionaRepo()
        {
            var fixture = new Fixture();
            var entidades = fixture.CreateMany<T>().ToArray();
            var target = CriarTarget();
            var repo = target.Repo;
            const string nome = "teste123";
            A.CallTo(() => repo.BuscarPorNome(nome))
                .Returns(entidades);

            var res = target.BuscarPorNome(nome).ToArray();

            CollectionAssert.AreEquivalent(entidades, res);
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

        internal virtual BusinessObject<T> CriarTarget()
        {
            return new BusinessObject<T>(
                A.Fake<IRepository<T>>());
        }

    }
}
