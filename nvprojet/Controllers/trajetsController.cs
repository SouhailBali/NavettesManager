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
    [RoutePrefix("trajet")]
    public class trajetsController : Controller
    {
        private TransportEntities db = new TransportEntities();

        // GET: trajets
        [Route("")]
        public ActionResult Index()
        {
            var trajets = db.trajets.Include(t => t.autocar);
            return View(trajets.ToList());
        }

        // GET: trajets/Details/5
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trajet trajet = db.trajets.Find(id);
            if (trajet == null)
            {
                return HttpNotFound();
            }
            return View(trajet);
        }

        // GET: trajets/Create
        [Route("ajouter")]
        public ActionResult Create()
        {
            ViewBag.id_aut = new SelectList(db.autocars, "id", "libele");
            return View();
        }

        // POST: trajets/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouter")]
        public ActionResult Create([Bind(Include = "id,ville_depart,ville_arriver,heure_dep,heur_arv,id_aut")] trajet trajet)
        {
           
            if (ModelState.IsValid)
            {
                var a= db.autocars.OrderByDescending(ss => ss.id).FirstOrDefault();
                trajet.id_aut = a.id;
                db.trajets.Add(trajet);
                db.SaveChanges();
             
                return Redirect("/");
            }

            ViewBag.id_aut = new SelectList(db.autocars, "id", "libele", trajet.id_aut);
            return View(trajet);
        }

        // GET: trajets/Edit/5
        [Route("modifier/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trajet trajet = db.trajets.Find(id);
            if (trajet == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_aut = new SelectList(db.autocars, "id", "libele", trajet.id_aut);
            return View(trajet);
        }

        // POST: trajets/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("modifier/{id}")]
        public ActionResult Edit([Bind(Include = "id,ville_depart,ville_arriver,heure_dep,heur_arv,id_aut")] trajet trajet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trajet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_aut = new SelectList(db.autocars, "id", "libele", trajet.id_aut);
            return View(trajet);
        }

        // GET: trajets/Delete/5
        [Route("supprimer/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trajet trajet = db.trajets.Find(id);
            if (trajet == null)
            {
                return HttpNotFound();
            }
            return View(trajet);
        }

        // POST: trajets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("supprimer/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            trajet trajet = db.trajets.Find(id);
            db.trajets.Remove(trajet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("choisir")]
        public ActionResult choix()
        {
            return View();
        }
        [Route("afficher")]
        public ActionResult afficher()
        {
            societe s = db.societes.Find(Session["societe"]);
            var offre = (
                from tr in db.trajets
                join au in db.autocars on tr.id_aut equals au.id
                join so in db.societes on au.id_ste equals so.id
                where so.id ==s.id
               
                select new offre
                {
                    nom_s = so.nom_s,
                    img = au.img,
                    wifi = au.wifi,
                    Nbr_place = au.Nbr_place,
                    ville_depart = tr.ville_depart,
                    ville_arriver = tr.ville_arriver,
                    heure_dep = tr.heure_dep,
                    heur_arv = tr.heur_arv,
                    id = tr.id

                }
            

                ).ToList();
            
            return View(offre);
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
