using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniRitter.Demo.DomainModel;
using UniRitter.Demo.BusinessLogic;

namespace UniRitterDemo.Controllers
{
    public class AutorController : Controller
    {
        private IBusinessObject<Autor> BO { get; set; }

        public AutorController(IBusinessObject<Autor> bo)
        {
            BO = bo;
        }

        //
        // GET: /Autor/

        public ActionResult Index()
        {
            return View();
        }

    }
}
