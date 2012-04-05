namespace UniRitter.Demo.DomainModel
{
    using System.ComponentModel.DataAnnotations;

    [Table("Autor")]
    public class Autor : IEntidade
    {
        [Column("AutorId")]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Nome { get; set; }
    }
}
