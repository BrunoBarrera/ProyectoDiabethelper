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
    public class RecetasController : Controller
    {
        private ProgramaHelperEntities db = new ProgramaHelperEntities();

        // GET: Recetas
        public ActionResult Index()
        {
            var recetas = db.Recetas.Include(r => r.RegistroDoctor).Include(r => r.RegistroPaciente);
            return View(recetas.ToList());
        }


        public ActionResult IndexPacientes()
        {
            var recetas = db.Recetas.Include(r => r.RegistroDoctor).Include(r => r.RegistroPaciente);
            return View(recetas.ToList());
        }



        // GET: Recetas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recetas recetas = db.Recetas.Find(id);
            if (recetas == null)
            {
                return HttpNotFound();
            }
            return View(recetas);
        }

        // GET: Recetas/Create
        public ActionResult Create()
        {
            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "NombreDoc");
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "NombreUsuario");
            return View();
        }

        // POST: Recetas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDReceta,CorreoPaciente,NombrePac,ApellidoPatPac,ApellidoMatPac,Fecha,CorreoDoc,InfoReceta")] Recetas recetas)
        {
            if (ModelState.IsValid)
            {
                db.Recetas.Add(recetas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "NombreDoc", recetas.CorreoDoc);
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "NombreUsuario", recetas.CorreoPaciente);
            return View(recetas);
        }

        // GET: Recetas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recetas recetas = db.Recetas.Find(id);
            if (recetas == null)
            {
                return HttpNotFound();
            }
            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "NombreDoc", recetas.CorreoDoc);
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "NombreUsuario", recetas.CorreoPaciente);
            return View(recetas);
        }

        // POST: Recetas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDReceta,CorreoPaciente,NombrePac,ApellidoPatPac,ApellidoMatPac,Fecha,CorreoDoc,InfoReceta")] Recetas recetas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recetas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "NombreDoc", recetas.CorreoDoc);
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "NombreUsuario", recetas.CorreoPaciente);
            return View(recetas);
        }

        // GET: Recetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recetas recetas = db.Recetas.Find(id);
            if (recetas == null)
            {
                return HttpNotFound();
            }
            return View(recetas);
        }

        // POST: Recetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recetas recetas = db.Recetas.Find(id);
            db.Recetas.Remove(recetas);
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
