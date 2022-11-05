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
    [RoutePrefix("demande")]
    public class demandesController : Controller
    {
        private TransportEntities db = new TransportEntities();

        [Route("")]
        public ActionResult Index()
        {
            var demandes = db.demandes.Include(d => d.client);
            return View(demandes.ToList());
        }

        [Route("ajouter")]
        public ActionResult Create()
        {
            ViewBag.id_cli = new SelectList(db.clients, "id", "nom_cli");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouter")]
        public ActionResult Create([Bind(Include = "ville_d,ville_a,heure_d,heur_a,id_cli")] demande demande)
        {
           
            if (ModelState.IsValid)
            {
               
                db.demandes.Add(demande);
                db.SaveChanges();
                return Redirect("/abonnement");
            }

        
            return View(demande);
        }
        [Route("confirmer/{id}")]
      public ActionResult addtrajet( int id)
        {
           demande d= db.demandes.Find(id);
            var aut = db.autocars.OrderByDescending(ss => ss.id).FirstOrDefault();
               string vd = d.ville_d;
                 string va = d.ville_a;
            string hd = d.heure_d;
            string ha = d.heur_a;
            trajet t = new trajet
            {
                ville_depart=vd,
                ville_arriver=va,
                heure_dep=hd,
                heur_arv=ha,
                id_aut=(int)aut.id

            };
            db.trajets.Add(t);
            db.SaveChanges();
            return Redirect("/");
        }
      

    }
}