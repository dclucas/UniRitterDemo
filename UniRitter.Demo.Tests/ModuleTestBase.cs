using System;
using NUnit.Framework;
using Ninject;
using UniRitter.Demo.DataAccessLogic;
using Ninject.Modules;

namespace UniRitter.Demo.Tests
{
    public class ModuleTestBase<TModule>
        where TModule : NinjectModule, new()
    {
        public virtual void Resolve_TipoExistente_ResolveCorretamente(Type tipo)
        {
            var target = new TModule();
            var kernel = new StandardKernel(target);
            CarregarModulos(kernel);
            var res = kernel.Get(tipo);
            Assert.NotNull(res);
        }

        public virtual void Resolve_TipoExterno_NaoResolve(Type tipo)
        {
            var target = new TModule();
            var kernel = new StandardKernel(target);
            CarregarModulos(kernel);
            var res = kernel.TryGet(tipo);
            Assert.Null(res);
        }

        protected virtual void CarregarModulos(IKernel kernel)
        {
        }
    }
}