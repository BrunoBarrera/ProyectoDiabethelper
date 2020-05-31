using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoDiabetC3.Models;

namespace ProyectoDiabetC3.Controllers
{
    public class NivelesController : Controller
    {
        
        private ProgramaHelperEntities db = new ProgramaHelperEntities();

        // GET: Niveles
        public ActionResult Index()
        {
            var niveles = db.Niveles.Include(n => n.RegistroDoctor).Include(n => n.RegistroPaciente);
            return View(niveles.ToList());
        }

        // GET: Niveles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Niveles niveles = db.Niveles.Find(id);
            if (niveles == null)
            {
                return HttpNotFound();
            }
            return View(niveles);
        }

        // GET: Niveles/Create
        public ActionResult Create()
        {
            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "CorreoDoc");
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "CorreoPaciente");
            return View();
        }

        // POST: Niveles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDNiveles,CorreoPaciente,NombrePac,ApellidoPatPac,ApellidoMatPac,Enfermedad,Niveles_glucosa,Niveles_presion,Fecha,CorreoDoc")] Niveles niveles)
        {
            if (ModelState.IsValid)
            {
                db.Niveles.Add(niveles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "CorreoDoc", niveles.CorreoDoc);
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "CorreoPaciente", niveles.CorreoPaciente);
            return View(niveles);
        }

        // GET: Niveles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Niveles niveles = db.Niveles.Find(id);
            if (niveles == null)
            {
                return HttpNotFound();
            }


            //Cosa que arreglo QUIROZ
            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "CorreoDoc", niveles.CorreoDoc);
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "CorreoPaciente", niveles.CorreoPaciente);
            ViewBag.correo = HttpContext.Session["correo"].ToString();
            return View(niveles);
        }

        // POST: Niveles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDNiveles,CorreoPaciente,NombrePac,ApellidoPatPac,ApellidoMatPac,Enfermedad,Niveles_glucosa,Niveles_presion,Fecha,CorreoDoc")] Niveles niveles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(niveles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "CorreoDoc", niveles.CorreoDoc);
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "CorreoPaciente", niveles.CorreoPaciente);
            return View(niveles);
        }

        // GET: Niveles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Niveles niveles = db.Niveles.Find(id);
            if (niveles == null)
            {
                return HttpNotFound();
            }
            return View(niveles);
        }

        // POST: Niveles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Niveles niveles = db.Niveles.Find(id);
            db.Niveles.Remove(niveles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
