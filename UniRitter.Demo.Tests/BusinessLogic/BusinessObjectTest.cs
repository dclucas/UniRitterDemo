using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UniRitter.Demo.BusinessLogic;
using UniRitter.Demo.DomainModel;
using UniRitter.Demo.DataAccessLogic;
using FakeItEasy;

namespace UniRitter.Demo.Tests.BusinessLogic
{
    [TestFixture]
    public class BusinessObjectTest
    {
        class BusinessObjectLocal : BusinessObject<Autor, IRepository<Autor>>
        {
            public BusinessObjectLocal(IRepository<Autor> repo)
                : base(repo)
            {
            }
        }

        [Test]
        public void Inserir_Redireciona()
        {
            var repo = A.Fake<IRepository<Autor>>();
            var target = new BusinessObjectLocal(repo);
            Autor autor = new Autor();
            A.CallTo(() => repo.Inserir(autor)).DoesNothing();
            target.Inserir(autor);
            A.CallTo(() => repo.Inserir(autor)).MustHaveHappened();
        }
    }
}
