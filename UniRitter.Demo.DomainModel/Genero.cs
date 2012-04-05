namespace UniRitter.Demo.DomainModel
{
    using System.ComponentModel.DataAnnotations;

    [Table("Genero")]
    public class Genero : IEntidade
    {
        [Column("GeneroId")]
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 1)]
        public string Nome { get; set; }
    }
}
