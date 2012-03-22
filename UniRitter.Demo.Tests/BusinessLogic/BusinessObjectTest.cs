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
            var repo = A.Fake<IRepository<T>>();
            var target = new BusinessObject<T>(repo);

            target.Inserir(entidade);

            A.CallTo(() => repo.Inserir(entidade))
                .MustHaveHappened();
        }

        [Test]
        public void BuscarPorNome_RedirecionaRepo()
        {
            var fixture = new Fixture();
            var entidades = fixture.CreateMany<T>().ToArray();
            var repo = A.Fake<IRepository<T>>();
            var target = new BusinessObject<T>(repo);
            var nome = "teste123";
            A.CallTo(() => repo.BuscarPorNome(nome))
                .Returns(entidades);

            var res = target.BuscaPorNome(nome).ToArray();

            CollectionAssert.AreEquivalent(entidades, res);
        }
    }
}
