using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniRitterDemo.Services.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract]
    public class EntityDataContract
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nome { get; set; }
    }
}