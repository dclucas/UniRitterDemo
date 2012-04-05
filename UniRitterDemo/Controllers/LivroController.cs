using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moo;
using UniRitter.Demo.BusinessLogic;
using UniRitter.Demo.DomainModel;

namespace UniRitterDemo.Controllers
{
    using UniRitterDemo.Models;

    public class LivroController : Controller
    {
        public IBusinessObject<Livro> BO { get; set; }

        public IMappingRepository MappingRepo { get; set; }

        public LivroController(
            IBusinessObject<Livro> bo,
            IMappingRepository mappingRepo)
        {
            BO = bo;
            MappingRepo = mappingRepo;
        }

        public ActionResult Index()
        {
            var entidades = BO.BuscarTodos();
            var mapper = MappingRepo.ResolveMapper<Livro, LivroIndexModel>();
            var models = mapper.MapMultiple(entidades);
            return View(models);
        }

    }
}
