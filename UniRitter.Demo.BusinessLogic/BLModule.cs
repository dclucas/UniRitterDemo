namespace UniRitter.Demo.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Ninject.Modules;

    using UniRitter.Demo.DomainModel;

    public class BLModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IBusinessObject<Livro>>().To<LivroBO>();
            this.Bind<IBusinessObject<Autor>>().To<BusinessObject<Autor>>();
            this.Bind<IBusinessObject<Genero>>().To<BusinessObject<Genero>>();
        }
    }
}
