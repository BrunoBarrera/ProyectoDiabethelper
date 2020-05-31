using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using ProyectoDiabetC3.Models;

namespace ProyectoDiabetC3.Controllers
{
    public class RegistroPacientesController : Controller
    {
        private ProgramaHelperEntities db = new ProgramaHelperEntities();

        // GET: RegistroPacientes
        public ActionResult Index()
        {
            return View(db.RegistroPaciente.ToList());
        }

        // GET: RegistroPacientes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroPaciente registroPaciente = db.RegistroPaciente.Find(id);
            if (registroPaciente == null)
            {
                return HttpNotFound();
            }
            return View(registroPaciente);
        }

        // GET: RegistroPacientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistroPacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NombreUsuario,ApellidoPatPac,ApellidoMatPac,CorreoPaciente,ContraseñaPaciente")] RegistroPaciente registroPaciente)
        {
            if (ModelState.IsValid)
            {

                db.RegistroPaciente.Add(registroPaciente);
                db.SaveChanges();
                //return RedirectToAction("Index");

                //Cosa que ayudo Q


                ExpedientePaciente expedientePaciente = new ExpedientePaciente();
                expedientePaciente.CorreoPaciente = registroPaciente.CorreoPaciente;

                return RedirectToAction("Create", "ExpedientePacientes",expedientePaciente);
            }
            //,registroPaciente.CorreoPaciente
            return View(registroPaciente);
        }

        // GET: RegistroPacientes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroPaciente registroPaciente = db.RegistroPaciente.Find(id);
            if (registroPaciente == null)
            {
                return HttpNotFound();
            }
            return View(registroPaciente);
        }

        // POST: RegistroPacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NombreUsuario,ApellidoPatPac,ApellidoMatPac,CorreoPaciente,ContraseñaPaciente")] RegistroPaciente registroPaciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registroPaciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registroPaciente);
        }

        // GET: RegistroPacientes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroPaciente registroPaciente = db.RegistroPaciente.Find(id);
            if (registroPaciente == null)
            {
                return HttpNotFound();
            }
            return View(registroPaciente);
        }

        // POST: RegistroPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RegistroPaciente registroPaciente = db.RegistroPaciente.Find(id);
            db.RegistroPaciente.Remove(registroPaciente);
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


        public ActionResult LoginPaciente(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }



        [HttpPost]
        public ActionResult LoginPaciente(string email, string password)
        {
            

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                ProgramaHelperEntities db = new ProgramaHelperEntities();
                var user = db.RegistroPaciente.FirstOrDefault(e => e.CorreoPaciente == email && e.ContraseñaPaciente == password);


                



                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.CorreoPaciente, true);
                    //IMPORTANTE PROBAR       HttpContext.Session.Add("correo", user.CorreoPaciente);
                    Session["correo"] = user.CorreoPaciente;

                    //Esto te ayuda guardar el dato que almacenaste
                    //Session["contrasena"] = user.ContraseñaPaciente;

                    return RedirectToAction("Index", "MenuPacientes");
                }
                else
                {
                    return RedirectToAction("LoginPaciente", new { message = "Usuario o contraseña incorrectos" });
                }
            }
            else
            {
                return RedirectToAction("LoginPaciente", new { message = "Llena los campos para poder iniciar sesion" });

            }
            
        }

            [Authorize]
            public ActionResult Logout()
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("LoginPaciente");
            }


        }

    }


