using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UniRitterDemo.Services
{
    using UniRitterDemo.Services.DataContracts;

    [ServiceContract]
    public interface IAutorService
    {
        [OperationContract]
        AutorDataContract BuscarPorNome(string nome);

        [OperationContract]
        AutorDataContract[] BuscarTodos();
    }
}
