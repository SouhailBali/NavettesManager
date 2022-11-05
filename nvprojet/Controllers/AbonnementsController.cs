using nvprojet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Net;



namespace nvprojet.Controllers
{
   
    [RoutePrefix("Abonnement")]
    public class AbonnementsController : Controller
    {
        public TransportEntities db = new TransportEntities();
        // GET: Abonnements
        [Route("")]
        public ActionResult Index()
        {
            var offre = (
                from tr in db.trajets join au in db.autocars on tr.id_aut equals au.id join so in db.societes on au.id_ste equals so.id
              
                select new offre
                {
                    nom_s=so.nom_s,img=au.img,wifi=au.wifi,Nbr_place=au.Nbr_place,ville_depart=tr.ville_depart,ville_arriver=tr.ville_arriver,heure_dep=tr.heure_dep,heur_arv=tr.heur_arv,id=tr.id
                    
                }


                ).ToList();
            return View(offre);
        }
        [HttpPost]
        [Route("effectuer/{id_tra}")]
        public ActionResult effectuerabonnement( int id_tra)
        {

            abennement ab = new abennement
            {

                date_debut = DateTime.Parse(Request.Form["datedebut"]),
                date_fin = DateTime.Parse(Request.Form["datefin"]),
                id_client = (int)Session["client"],
                id_trajet = id_tra


            };
            db.abennements.Add(ab);
            db.SaveChanges();
            ViewBag.Message = "Email not found or matched"; //"votre demande d'abonnement est faite par succée !!!!!";
            return RedirectToAction("Index");
        }
    }

}