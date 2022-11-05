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
    [RoutePrefix("client")]
    public class clientsController : Controller
    {
        private TransportEntities db = new TransportEntities();

        // GET: clients
        [Route("")]
        public ActionResult Index()
        {
            return View(db.clients.ToList());
        }

        // GET: clients/Details/5
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: clients/Create
        [Route("ajouter")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: clients/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouter")]
        public ActionResult Create([Bind(Include = "nom_cli,prenom_cli,adresse,tel,login_cli,mdp,email")] client client)
        {
            if (ModelState.IsValid)
            {
                db.clients.Add(client);
                db.SaveChanges();
                return Redirect("/abonnement");
            }

            return View(client);
        }

        // GET: clients/Edit/5
        [Route("modifier/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: clients/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("modifier/{id}")]
        public ActionResult Edit([Bind(Include = "id,nom_cli,prenom_cli,adresse,tel,login_cli,mdp,email")] client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: clients/Delete/5
        [Route("supprimer/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: clients/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("supprimer/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            client client = db.clients.Find(id);
            db.clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("cnxclient")]
        public ActionResult connexion()
        {
            return View();
        }

        [Route("cnxclient")]
        [HttpPost]
        public ActionResult connexion([Bind(Include = "id,login_cli,mdp")] client a)
        {
            var trouve = db.clients.Where(s => s.login_cli == a.login_cli && s.mdp == a.mdp).FirstOrDefault();
            if (trouve != null)
            {
                Session["client"] = trouve.id;
                return Redirect("/abonnement");
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
