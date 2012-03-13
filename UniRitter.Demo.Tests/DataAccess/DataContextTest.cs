using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UniRitter.Demo.DataAccessLogic;
using UniRitter.Demo.DomainModel;

namespace UniRitter.Demo.Tests.DataAccess
{
    [TestFixture]
    public class DataContextTest
    {
        [Test]
        public void Add_AddsToDb()
        {
            DataContext target = new DataContext();
            Autor a = new Autor();
            string nome = "Autor" + Guid.NewGuid().ToString();
            a.Nome = nome;
            target.Set<Autor>().Add(a);
            target.SaveChanges();

            DataContext second = new DataContext();
            var res = second.Set<Autor>().First(o => o.Nome.Equals(nome));
            Assert.IsNotNull(res);
        }
    }
}
