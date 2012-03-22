using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Ninject;
using UniRitter.Demo.DataAccessLogic;
using UniRitter.Demo.DomainModel;
using UniRitter.Demo.BusinessLogic;

namespace UniRitter.Demo.Tests.DataAccess
{
    [TestFixture]
    public class DalModuleTest : ModuleTestBase<DalModule>
    {
        [TestCase(typeof(IRepository<Autor>))]
        [TestCase(typeof(IRepository<Genero>))]
        [TestCase(typeof(IRepository<Livro>))]
        [TestCase(typeof(IDataContext))]
        public override void Resolve_TipoExistente_ResolveCorretamente(Type tipo)
        {
            base.Resolve_TipoExistente_ResolveCorretamente(tipo);
        }

        [TestCase(typeof(IBusinessObject<Autor>))]
        [TestCase(typeof(IBusinessObject<Genero>))]
        [TestCase(typeof(IBusinessObject<Livro>))]
        public override void Resolve_TipoExterno_NaoResolve(Type tipo)
        {
            base.Resolve_TipoExterno_NaoResolve(tipo);
        }
    }
}
