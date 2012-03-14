using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRitter.Demo.DomainModel;
using FakeItEasy;
using System.Data.Entity;
using UniRitter.Demo.DataAccessLogic;
using NUnit.Framework;

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
    }
}
