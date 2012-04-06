namespace UniRitterDemo.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Moo;
    using UniRitter.Demo.BusinessLogic;
    using UniRitter.Demo.DomainModel;
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
            this.BO = bo;
            this.MappingRepo = mappingRepo;
            this.LookupHelper = lookupHelper;
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
            var models = from l in lookups select mapper.Map(l);
            ViewData.Add(lookupName, models);
        }

        public ActionResult Edit(int id)
        {
            return MostrarModel<LivroEditModel>(id);
        }

        private ActionResult MostrarModel<T>(int id)
        {
            this.PopularLookups();
            var entidade = this.BO.BuscarPorId(id);
            var mapper = this.MappingRepo.ResolveMapper(typeof(Livro), typeof(T));
            var model = mapper.Map(entidade);
            return this.View(model);
        }

        private void PopularLookups()
        {
            this.PopularLookup<Autor>();
            this.PopularLookup<Genero>();
        }

        public ActionResult Create()
        {
            this.PopularLookups();
            return this.View();
        }
    }
}
