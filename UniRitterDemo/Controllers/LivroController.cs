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

        public ILookupHelper LookupHelper { get; set; }

        public LivroController(
            IBusinessObject<Livro> bo,
            IMappingRepository mappingRepo,
            ILookupHelper lookupHelper)
        {
            BO = bo;
            MappingRepo = mappingRepo;
            LookupHelper = lookupHelper;
        }

        public ActionResult Index()
        {
            var entidades = this.BO.BuscarTodos();
            var mapper = this.MappingRepo.ResolveMapper<Livro, LivroIndexModel>();
            var models = mapper.MapMultiple(entidades);
            return View(models);
        }

        private void PopularLookup<T>()
            where T : class, IEntidade
        {
            var lookups = this.LookupHelper.BuscarTodos<T>();
            var mapper = this.MappingRepo.ResolveMapper(typeof(T), typeof(LookupModel));
            var lookupName = string.Format("{0}Lookup", typeof(T).Name);
            ViewData.Add(lookupName, mapper.Map(lookups));
        }

        public ActionResult Edit(int id)
        {
            return MostrarModel<LivroEditModel>(id);
        }

        private ActionResult MostrarModel<T>(int id)
        {
            PopularLookups();
            var entidade = BO.BuscarPorId(id);
            var mapper = MappingRepo.ResolveMapper(typeof(Livro), typeof(T));
            var model = mapper.Map(entidade);
            return this.View(model);
        }

        private void PopularLookups()
        {
            this.PopularLookup<Autor>();
            this.PopularLookup<Genero>();
        }
    }
}
