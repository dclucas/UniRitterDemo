namespace UniRitterDemo.Console
{
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var svc = new AutorServiceReference.AutorServiceClient();
            var res = svc.BuscarTodos().OrderBy(a => a.Nome);
            System.Console.WriteLine("Autores");
            foreach (var a in res)
            {
                System.Console.WriteLine(a.Nome);
            }
            System.Console.ReadKey();
        }
    }
}
