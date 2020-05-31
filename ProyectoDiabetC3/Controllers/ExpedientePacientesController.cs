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
    public class ExpedientePacientesController : Controller
    {
        private ProgramaHelperEntities db = new ProgramaHelperEntities();

        // GET: ExpedientePacientes
        public ActionResult Index()
        {
            var expedientePaciente = db.ExpedientePaciente.Include(e => e.RegistroDoctor).Include(e => e.RegistroPaciente);
            return View(expedientePaciente.ToList());
        }




        // GET: ExpedientePacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpedientePaciente expedientePaciente = db.ExpedientePaciente.Find(id);
            if (expedientePaciente == null)
            {
                return HttpNotFound();
            }
            return View(expedientePaciente);
        }

        // GET: ExpedientePacientes/Create
        public ActionResult Create()
        {
            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "CorreoDoc");
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "CorreoPaciente");
            return View();
        }

        // POST: ExpedientePacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDExpediente,CorreoPaciente,NombrePac,ApellidoPatPac,ApellidoMatPac,Sexo,Edad,Enfermedad,Peso,TipoDeSangre,TratamientosActuales,TratamientosAnteriores,CorreoDoc")] ExpedientePaciente expedientePaciente)
        {
            if (ModelState.IsValid)
            {
                db.ExpedientePaciente.Add(expedientePaciente);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index","MenuPacientes");

            }

            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "CorreoDoc", expedientePaciente.CorreoDoc);
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "CorreoPaciente", expedientePaciente.CorreoPaciente);
            return View(expedientePaciente);
        }

        // GET: ExpedientePacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpedientePaciente expedientePaciente = db.ExpedientePaciente.Find(id);
            if (expedientePaciente == null)
            {
                return HttpNotFound();
            }
            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "CorreoDoc", expedientePaciente.CorreoDoc);
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "CorreoPaciente", expedientePaciente.CorreoPaciente);
            return View(expedientePaciente);
        }

        // POST: ExpedientePacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDExpediente,CorreoPaciente,NombrePac,ApellidoPatPac,ApellidoMatPac,Sexo,Edad,Enfermedad,Peso,TipoDeSangre,TratamientosActuales,TratamientosAnteriores,CorreoDoc")] ExpedientePaciente expedientePaciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expedientePaciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "CorreoDoc", expedientePaciente.CorreoDoc);
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "CorreoPaciente", expedientePaciente.CorreoPaciente);
            return View(expedientePaciente);
        }

        // GET: ExpedientePacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpedientePaciente expedientePaciente = db.ExpedientePaciente.Find(id);
            if (expedientePaciente == null)
            {
                return HttpNotFound();
            }
            return View(expedientePaciente);
        }

        // POST: ExpedientePacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpedientePaciente expedientePaciente = db.ExpedientePaciente.Find(id);
            db.ExpedientePaciente.Remove(expedientePaciente);
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
