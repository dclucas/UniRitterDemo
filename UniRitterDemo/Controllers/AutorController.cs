using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moo;
using UniRitter.Demo.DomainModel;
using UniRitter.Demo.BusinessLogic;
using UniRitterDemo.Models;

namespace UniRitterDemo.Controllers
{
    public class AutorController : Controller
    {
        public IBusinessObject<Autor> BO { get; private set; }

        public IMapper<Autor, AutorModel> Mapper { get; private set; }

        public AutorController(
            IBusinessObject<Autor> bo,
            IMapper<Autor, AutorModel> mapper)
        {
            BO = bo;
            Mapper = mapper;
        }

        //
        // GET: /Autor/

        public ActionResult Index()
        {
            var entidades = BO.BuscarTodos();
            var models = Mapper.MapMultiple(entidades);
            return View(models);
        }

    }
}
