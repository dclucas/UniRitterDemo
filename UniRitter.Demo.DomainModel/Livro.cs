namespace UniRitter.Demo.DomainModel
{
    using System.ComponentModel.DataAnnotations;

    [Table("Livro")]
    public class Livro : IEntidade
    {
        [Column("LivroId")]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Fonte { get; set; }

        public int AnoPublicacao { get; set; }

        [StringLength(40)]
        public string Editora { get; set; }

        public Autor Autor { get; set; }

        public Genero Genero { get; set; }

        [ForeignKey("Autor")]
        public int AutorId { get; set; }

        [ForeignKey("Genero")]
        public int GeneroId { get; set; }
    }
}
