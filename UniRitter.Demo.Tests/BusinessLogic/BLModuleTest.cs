using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using UniRitter.Demo.BusinessLogic;
using NUnit.Framework;
using UniRitter.Demo.DomainModel;
using UniRitter.Demo.DataAccessLogic;

namespace UniRitter.Demo.Tests.BusinessLogic
{
    public class BLModuleTest : ModuleTestBase<BLModule>
    {
        [TestCase(typeof(IBusinessObject<Autor>))]
        [TestCase(typeof(IBusinessObject<Genero>))]
        [TestCase(typeof(IBusinessObject<Livro>))]
        public override void Resolve_TipoExistente_ResolveCorretamente(Type tipo)
        {
            base.Resolve_TipoExistente_ResolveCorretamente(tipo);
        }

        [TestCase(typeof(IEntidade))]
        public override void Resolve_TipoExterno_NaoResolve(Type tipo)
        {
            base.Resolve_TipoExterno_NaoResolve(tipo);
        }

        protected override void CarregarModulos(Ninject.IKernel kernel)
        {
            base.CarregarModulos(kernel);
            var module = new DalModule();
            kernel.Load(new INinjectModule[] { module });
        }
    }
}
