using Moo;
using Ninject.Activation;
using Ninject.Modules;

namespace UniRitterDemo
{
    public class BLModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IMapper<,>))
                .ToMethod(
                    ResolveMapper);
        }

        private object ResolveMapper(IContext ctx)
        {
            var t = ctx.Request.Service.GetGenericArguments();
            return MappingRepository.Default.ResolveMapper(t[0], t[1]);
        }
    }
}