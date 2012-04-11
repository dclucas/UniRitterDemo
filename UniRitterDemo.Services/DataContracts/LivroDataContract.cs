namespace UniRitterDemo.Services.DataContracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Runtime.Serialization;

    [DataContract]
    public class LivroDataContract : EntityDataContract
    {
        [DataMember]
        public string Fonte { get; set; }

        [DataMember]
        public string Editora { get; set; }

        [DataMember]
        public int AnoPublicacao { get; set; }

        [DataMember]
        public AutorDataContract Autor { get; set; }

        [DataMember]
        public GeneroDataContract Genero { get; set; }
    }
}