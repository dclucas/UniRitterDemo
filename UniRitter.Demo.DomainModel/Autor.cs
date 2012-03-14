using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace UniRitter.Demo.DomainModel
{
    public class Autor : IEntidade
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }
}
