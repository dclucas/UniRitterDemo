namespace UniRitter.Demo.IntegratedTests.DataAccessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using UniRitter.Demo.DataAccessLogic;
    using UniRitter.Demo.DomainModel;

    //[Ignore]
    [TestFixture(typeof(Autor))]
    [TestFixture(typeof(Genero))]
    [TestFixture(typeof(Livro))]
    public class DataContextTest<TEntidade> where TEntidade : class, IEntidade, new()
    {
        [Test]
        public void Add_AddsToDb()
        {
            DataContext target = new DataContext();
            var entidade = CriarEntidade();
            target.Set<TEntidade>().Add(entidade);
            target.SaveChanges();

            DataContext second = new DataContext();
            var res = second.Set<TEntidade>().First(o => o.Nome.Equals(entidade.Nome));
            Assert.IsNotNull(res);
        }

        public TEntidade CriarEntidade()
        {
            var t = typeof(TEntidade);
            var entidade = new TEntidade();
            entidade.Nome = t.Name + Guid.NewGuid().ToString();
            return entidade;
        }
    }
}
