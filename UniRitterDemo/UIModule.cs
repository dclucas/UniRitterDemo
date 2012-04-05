namespace UniRitterDemo
{
    using Moo;
    using Ninject.Activation;
    using Ninject.Modules;

    public class UIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMappingRepository>().ToMethod(c => MappingRepository.Default);
            Bind(typeof(IMapper<,>))
                .ToMethod(ResolveMapper);
        }

        private object ResolveMapper(IContext ctx)
        {
            var t = ctx.Request.Service.GetGenericArguments();
            return MappingRepository.Default.ResolveMapper(t[0], t[1]);
        }
    }
}