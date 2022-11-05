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
    [RoutePrefix("societe")]
    public class societesController : Controller
    {
        private TransportEntities db = new TransportEntities();

        // GET: societes
        [Route("")]
        public ActionResult Index()
        {
            return View(db.societes.ToList());
        }

        // GET: societes/Details/5
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            societe societe = db.societes.Find(id);
            if (societe == null)
            {
                return HttpNotFound();
            }
            return View(societe);
        }

        // GET: societes/Create
        [Route("ajouter")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: societes/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouter")]
        public ActionResult Create([Bind(Include = "id,nom_s,siege,pattente,fix,fax,email_s,logn_s,passwd_s")] societe societe)
        {
            if (ModelState.IsValid)
            {
                db.societes.Add(societe);
                db.SaveChanges();
                return Redirect("/autocars/ajouter");
            }

            return View(societe);
        }

        // GET: societes/Edit/5
        [Route("modifier/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            societe societe = db.societes.Find(id);
            if (societe == null)
            {
                return HttpNotFound();
            }
            return View(societe);
        }

        // POST: societes/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("modifier/{id}")]
        public ActionResult Edit([Bind(Include = "id,nom_s,siege,pattente,fix,fax,email_s,logn_s,passwd_s")] societe societe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(societe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(societe);
        }

        // GET: societes/Delete/5
        [Route("supprimer/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            societe societe = db.societes.Find(id);
            if (societe == null)
            {
                return HttpNotFound();
            }
            return View(societe);
        }

        // POST: societes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("supprimer/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            societe societe = db.societes.Find(id);
            db.societes.Remove(societe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("cnxsociete")]
        public ActionResult connect()
        {
            return View();
        }

        [Route("cnxsociete")]
        [HttpPost]
        public ActionResult connect([Bind(Include = "id,logn_s,passwd_s")] societe societe)
        {
            var trouve = db.societes.Where(s => s.logn_s == societe.logn_s && s.passwd_s == societe.passwd_s).FirstOrDefault();
            if (trouve != null)
            {
                Session["societe"]= trouve.id;
                Session["nom_s"] = trouve.nom_s;
                return Redirect("/trajet/afficher");
            }

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
