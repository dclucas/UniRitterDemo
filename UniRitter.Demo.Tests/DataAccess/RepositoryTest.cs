using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRitter.Demo.DomainModel;
using FakeItEasy;
using System.Data.Entity;
using UniRitter.Demo.DataAccessLogic;
using NUnit.Framework;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Linq.Expressions;
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
            var db = A.Fake<IDataContext>();
            var set = A.Fake<IDbSet<TEntidade>>();
            var target = new Repository<TEntidade>(db);
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
            var db = A.Fake<IDataContext>();
            var set = A.Fake<IDbSet<TEntidade>>();
            var target = new Repository<TEntidade>(db);
            var entidade = new TEntidade();

            target.Atualizar(entidade);

            A.CallTo(() => db.SetarEstado<TEntidade>(entidade, EntityState.Modified))
                .MustHaveHappened();

            A.CallTo(() => db.SaveChanges()).MustHaveHappened();
        }

        [Test]
        public void Remover_Redireciona()
        {
            var db = A.Fake<IDataContext>();
            var set = A.Fake<IDbSet<TEntidade>>();
            var target = new Repository<TEntidade>(db);
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
            var db = A.Fake<IDataContext>();
            var target = new Repository<TEntidade>(db);
            var entidade = new TEntidade();

            var key = 152;
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
            var db = A.Fake<IDataContext>();
            var inc = new string[] { "abc", "def" };
            var entidades = new TEntidade[5];
            A.CallTo(() => db.Buscar<TEntidade>(inc))
                .Returns(entidades);
            var target = new Repository<TEntidade>(db);

            var res = target.Buscar(inc);

            A.CallTo(() => db.Buscar<TEntidade>(inc))
                .MustHaveHappened();
            Assert.AreEqual(entidades, res);
        }

        [Test]
        public void BuscarTodos_ComInclusoes_Redireciona()
        {
            var db = A.Fake<IDataContext>();
            var entidades = A.Fake<IDbSet<TEntidade>>();
            A.CallTo(() => db.BuscarTodos<TEntidade>())
                .Returns(entidades);
            var target = new Repository<TEntidade>(db);

            var res = target.BuscarTodos();

            A.CallTo(() => db.BuscarTodos<TEntidade>())
                .MustHaveHappened();
            Assert.AreEqual(entidades, res);
        }

        [Test]
        public void Dispose_InvocaDispose()
        {
            var db = A.Fake<IDataContext>();
            var target = new Repository<TEntidade>(db);

            target.Dispose();

            A.CallTo(() => db.Dispose())
                .MustHaveHappened();
        }

        [Test]
        public void BuscarPorNome_NomeExato_RetornaCorretamente()
        {
            var db = A.Fake<IDataContext>();
            var target = new Repository<TEntidade>(db);
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
