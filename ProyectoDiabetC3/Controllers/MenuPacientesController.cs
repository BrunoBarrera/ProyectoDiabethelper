using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDiabetC3.Controllers
{


    [Authorize]
    public class MenuPacientesController : Controller
    {
        // GET: MenuPacientes
        public ActionResult Index()
        {
            ViewBag.correo = Session["correo"].ToString();
            return View();
        }
    }
}