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
        public void BuscarPorNome_Redireciona()
        {
            var db = A.Fake<IDataContext>();
            var entities = new TEntidade[10];
            var target = new Repository<TEntidade>(db);

            var nome = "nome";

            Expression<Func<TEntidade, bool>> pred = 
                ((TEntidade x) => x.Nome.Contains(nome));
            A.CallTo(() => db.Buscar<TEntidade>(pred))
                .Returns(entities);

            var res = target.BuscarPorNome(nome);

            A.CallTo(() => db.Buscar<TEntidade>(
                A<Expression<Func<TEntidade, bool>>>
                .That.Matches(f =>
                    (f.Compile().Invoke(new TEntidade { Nome = nome }))
                    && !(f.Compile().Invoke(new TEntidade { Nome = "foo" }))
                )))
                .MustHaveHappened();

            Assert.AreEqual(entities, res);
        }
    }
}
