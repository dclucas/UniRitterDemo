namespace UniRitterDemo.Services
{
    using System;

    using Moo;

    using UniRitter.Demo.BusinessLogic;

    using UniRitterDemo.Services.DataContracts;
    using UniRitter.Demo.DomainModel;

    public class AutorService : BaseService<AutorDataContract, Autor>, IAutorService
    {
        public AutorService(IBusinessObject<Autor> bo, IMapper<AutorDataContract, Autor> dataContractMapper, IMapper<Autor, AutorDataContract> entityMapper)
            : base(bo, dataContractMapper, entityMapper)
        {
        }
    }
}
