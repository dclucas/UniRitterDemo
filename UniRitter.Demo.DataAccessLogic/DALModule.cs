using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;

namespace UniRitter.Demo.DataAccessLogic
{
    public class DalModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(typeof(IRepository<>))
                .To(typeof(Repository<>));

            this.Bind<IDataContext>()
                .To<DataContext>();
        }
    }
}
