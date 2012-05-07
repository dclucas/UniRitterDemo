namespace UniRitterDemo.Services
{
    using System;
    using System.Linq;

    using Moo;

    using UniRitter.Demo.BusinessLogic;
    using UniRitter.Demo.DomainModel;

    public class BaseService<TDataContract, TEntity>
        where TEntity : class, IEntidade
    {
        public IBusinessObject<TEntity> BO { get; set; }

        protected IMapper<TDataContract, TEntity> DataContractMapper { get; set; }

        protected IMapper<TEntity, TDataContract> EntityMapper { get; set; }

        public BaseService(
            IBusinessObject<TEntity> bo,
            IMapper<TDataContract, TEntity> dataContractMapper,
            IMapper<TEntity, TDataContract> entityMapper)
        {
            BO = bo;
            DataContractMapper = dataContractMapper;
            EntityMapper = entityMapper;
        }

        public TDataContract BuscarPorNome(string nome)
        {
            var res = BO.BuscarPorNome(nome);
            return (TDataContract)this.EntityMapper.Map(res);
        }

        public TDataContract[] BuscarTodos()
        {
            var res = BO.BuscarTodos();
            return EntityMapper.MapMultiple(res).ToArray();
        }
    }
}