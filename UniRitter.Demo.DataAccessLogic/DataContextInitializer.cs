// -----------------------------------------------------------------------
// <copyright file="DataContextInitializer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UniRitter.Demo.DataAccessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;

    using UniRitter.Demo.DomainModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class DataContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            base.Seed(context);

            Inserir<Genero>(
                context, 
                "Romance",
                "Poesia",
                "Teatro",
                "Biografia");

            Inserir<Autor>(
                context,
                "José Saramago",
                "Ítalo Calvino",
                "William Shakespeare",
                "Edgar Allan Poe",
                "Oscar Wilde",
                "Umberto Eco",
                "Bob Spitz",
                "Machado de Assis");

            context.SaveChanges();

            Inserir<Livro>(
                context,
                this.CriarLivro(context, 1947, "Terra do Pecado", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 1977, "Manual de Pintura e Caligrafia", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 1980, "Levantado do Chão", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 1982, "Memorial do Convento", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 1984, "O Ano da Morte de Ricardo Reis", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 1986, "A Jangada de Pedra", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 1989, "História do Cerco de Lisboa", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 1991, "O Evangelho Segundo Jesus Cristo", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 1995, "Ensaio Sobre a Cegueira", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 1997, "Todos os Nomes", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 2000, "A Caverna", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 2002, "O Homem Duplicado", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 2004, "Ensaio Sobre a Lucidez", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 2005, "As Intermitências da Morte", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 2008, "A Viagem do Elefante", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 2009, "Caim", "José Saramago", "Romance", "Livro"),
                this.CriarLivro(context, 2011, "Claraboia", "José Saramago", "Romance", "Livro"));

            context.SaveChanges();
        }

        private Livro CriarLivro(DataContext context, int ano, string nome, string autorNome, string generoNome, string fonte)
        {
            return new Livro()
                {
                    Nome = nome,
                    Autor = context.Autores.Single(a => a.Nome.Equals(autorNome)),
                    Genero = context.Generos.Single(g => g.Nome.Equals(generoNome)),
                    Fonte = fonte,
                    AnoPublicacao = ano,

                };
        }

        private void Inserir<T>(DataContext context, params string[] nomes)
            where T : class, IEntidade, new()
        {
            var q = from n in nomes select new T() { Nome = n };

            Inserir<T>(context, q.ToArray());
        }

        private void Inserir<T>(DataContext context, params T[] entidades)
            where T : class, IEntidade, new()
        {
            foreach (var e in entidades)
            {
                context.Set<T>().Add(e);
            }
        }

    }
}
