using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniRitter.Demo.DomainModel
{
    public interface IEntidade
    {
        int Id { get; set; }

        string Nome { get; set; }
    }
}
