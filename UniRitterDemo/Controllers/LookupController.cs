using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniRitterDemo.Controllers
{
    using Moo;

    using UniRitter.Demo.BusinessLogic;
    using UniRitter.Demo.DomainModel;

    public class LookupController : Controller
    {
        public ILookupHelper LookupHelper { get; set; }

        public IMappingRepository MappingRepo { get; set; }

        public ActionResult Index()
        {
            return this.View();
        }

        public LookupController(ILookupHelper lookupHelper, IMappingRepository mappingRepo)
        {
            LookupHelper = lookupHelper;
            MappingRepo = mappingRepo;
        }

        private JsonResult Listar<T>(string term)
            where T : class, IEntidade
        {
            var l = this.LookupHelper.BuscarPorNome<T>(term ?? string.Empty);
            var mapper = this.MappingRepo.ResolveMapper<IEntidade, JsonLookup>();
            var jl = mapper.MapMultiple(l);
            return this.Json(jl, JsonRequestBehavior.AllowGet);            
        }

        public JsonResult ListarAutores(string term)
        {
            return this.Listar<Autor>(term);
        }

        public JsonResult ListarGeneros(string term)
        {
            return this.Listar<Genero>(term);
        }

        public JsonResult ListarLivros(string term)
        {
            return this.Listar<Livro>(term);
        }

        public class JsonLookup
        {
            [Mapping(MappingDirections.Both, typeof(IEntidade), "Id")]
            public int id { get; set; }

            [Mapping(MappingDirections.Both, typeof(IEntidade), "Nome")]
            public string label { get; set; }
        }
    }
}
