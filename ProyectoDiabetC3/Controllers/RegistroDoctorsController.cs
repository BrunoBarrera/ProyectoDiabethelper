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
    public class RegistroDoctorsController : Controller
    {
        private ProgramaHelperEntities db = new ProgramaHelperEntities();

        // GET: RegistroDoctors
        public ActionResult Index()
        {
            return View(db.RegistroDoctor.ToList());
        }

        // GET: RegistroDoctors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroDoctor registroDoctor = db.RegistroDoctor.Find(id);
            if (registroDoctor == null)
            {
                return HttpNotFound();
            }
            return View(registroDoctor);
        }

        // GET: RegistroDoctors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistroDoctors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NombreDoc,ApellidoPatDoc,ApellidoMatDoc,CorreoDoc,Contraseña,CedulaProfesional,FormaciónAcademica")] RegistroDoctor registroDoctor)
        {
            if (ModelState.IsValid)
            {
                
                db.RegistroDoctor.Add(registroDoctor);
                db.SaveChanges();
                //Cosa que ayudo QUIROZ
                return RedirectToAction("Create", "ExpedientePacientes");
            }

            return View(registroDoctor);
        }

        // GET: RegistroDoctors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroDoctor registroDoctor = db.RegistroDoctor.Find(id);
            if (registroDoctor == null)
            {
                return HttpNotFound();
            }
            return View(registroDoctor);
        }

        // POST: RegistroDoctors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NombreDoc,ApellidoPatDoc,ApellidoMatDoc,CorreoDoc,Contraseña,CedulaProfesional,FormaciónAcademica")] RegistroDoctor registroDoctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registroDoctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registroDoctor);
        }

        // GET: RegistroDoctors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroDoctor registroDoctor = db.RegistroDoctor.Find(id);
            if (registroDoctor == null)
            {
                return HttpNotFound();
            }
            return View(registroDoctor);
        }

        // POST: RegistroDoctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RegistroDoctor registroDoctor = db.RegistroDoctor.Find(id);
            db.RegistroDoctor.Remove(registroDoctor);
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
