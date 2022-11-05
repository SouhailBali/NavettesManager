using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using nvprojet.Models;

namespace nvprojet.Controllers
{
    public class administrateursController : Controller
    {
        private TransportEntities db = new TransportEntities();

        // GET: administrateurs
        [Route("admin")]
        public ActionResult Index()
        {
            return View(db.administrateurs.ToList());
        }

        // GET: administrateurs/Details/5
        [Route("admin/details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            administrateur administrateur = db.administrateurs.Find(id);
            if (administrateur == null)
            {
                return HttpNotFound();
            }
            return View(administrateur);
        }

        // GET: administrateurs/Create
        [Route("admin/ajouter")]
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/ajouter")]
        public ActionResult Create([Bind(Include = "id,nom,prenom,logn,passwd")] administrateur administrateur)
        {
            if (ModelState.IsValid)
            {
                db.administrateurs.Add(administrateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(administrateur);
        }

        // GET: administrateurs/Edit/5
        [Route("admin/modifier/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            administrateur administrateur = db.administrateurs.Find(id);
            if (administrateur == null)
            {
                return HttpNotFound();
            }
            return View(administrateur);
        }

        // POST: administrateurs/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/modifier/{id}")]
        public ActionResult Edit([Bind(Include = "id,nom,prenom,logn,passwd")] administrateur administrateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(administrateur);
        }

        // GET: administrateurs/Delete/5
        [Route("admin/supprimer/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            administrateur administrateur = db.administrateurs.Find(id);
            if (administrateur == null)
            {
                return HttpNotFound();
            }
            return View(administrateur);
        }

        // POST: administrateurs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/supprimer/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            administrateur administrateur = db.administrateurs.Find(id);
            db.administrateurs.Remove(administrateur);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("seconnecter")]
        public ActionResult login()
        {
            return View();
        }

        [Route("seconnecter")]
        [HttpPost]
        public ActionResult login([Bind(Include = "logn,passwd")] administrateur a)
        {
            var trouve = db.administrateurs.Where(s => s.logn == a.logn && s.passwd ==a.passwd).FirstOrDefault();
            if (trouve != null)
            {
                return Redirect("/trajet");
            }

            return View();
        }
        [Route]
        public ActionResult acceuil()
        {
            return View();
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
