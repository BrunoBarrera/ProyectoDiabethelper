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
    public class CitasAgendadasController : Controller
    {
        private ProgramaHelperEntities db = new ProgramaHelperEntities();

        // GET: CitasAgendadas
        public ActionResult Index()
        {
            var citasAgendadas = db.CitasAgendadas.Include(c => c.RegistroDoctor).Include(c => c.RegistroPaciente);
            return View(citasAgendadas.ToList());
        }

        // GET: CitasAgendadas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CitasAgendadas citasAgendadas = db.CitasAgendadas.Find(id);
            if (citasAgendadas == null)
            {
                return HttpNotFound();
            }
            return View(citasAgendadas);
        }

        // GET: CitasAgendadas/Create
        public ActionResult Create()
        {
            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "NombreDoc");
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "NombreUsuario");
            return View();
        }

        // POST: CitasAgendadas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCita,CorreoPaciente,NombrePac,ApellidoPatPac,ApellidoMatPac,Fecha,CorreoDoc")] CitasAgendadas citasAgendadas)
        {
            if (ModelState.IsValid)
            {
                db.CitasAgendadas.Add(citasAgendadas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "NombreDoc", citasAgendadas.CorreoDoc);
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "NombreUsuario", citasAgendadas.CorreoPaciente);
            return View(citasAgendadas);
        }

        // GET: CitasAgendadas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CitasAgendadas citasAgendadas = db.CitasAgendadas.Find(id);
            if (citasAgendadas == null)
            {
                return HttpNotFound();
            }
            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "NombreDoc", citasAgendadas.CorreoDoc);
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "NombreUsuario", citasAgendadas.CorreoPaciente);
            return View(citasAgendadas);
        }

        // POST: CitasAgendadas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCita,CorreoPaciente,NombrePac,ApellidoPatPac,ApellidoMatPac,Fecha,CorreoDoc")] CitasAgendadas citasAgendadas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citasAgendadas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CorreoDoc = new SelectList(db.RegistroDoctor, "CorreoDoc", "NombreDoc", citasAgendadas.CorreoDoc);
            ViewBag.CorreoPaciente = new SelectList(db.RegistroPaciente, "CorreoPaciente", "NombreUsuario", citasAgendadas.CorreoPaciente);
            return View(citasAgendadas);
        }

        // GET: CitasAgendadas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CitasAgendadas citasAgendadas = db.CitasAgendadas.Find(id);
            if (citasAgendadas == null)
            {
                return HttpNotFound();
            }
            return View(citasAgendadas);
        }

        // POST: CitasAgendadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CitasAgendadas citasAgendadas = db.CitasAgendadas.Find(id);
            db.CitasAgendadas.Remove(citasAgendadas);
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
