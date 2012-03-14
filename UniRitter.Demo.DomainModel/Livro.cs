using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace UniRitter.Demo.DomainModel
{
    public class Livro : IEntidade
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Fonte { get; set; }

        public int AnoPublicacao { get; set; }

        public string Editora { get; set; }

        public Autor Autor { get; set; }

        public Genero Genero { get; set; }
    }
}
