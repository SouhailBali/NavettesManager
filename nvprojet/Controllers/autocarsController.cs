using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using nvprojet.Models;

namespace nvprojet.Controllers
{
    [RoutePrefix("autocars")]
    public class autocarsController : Controller
    {
        private TransportEntities db = new TransportEntities();

        // GET: autocars
        [Route("")]
        public ActionResult Index()
        {
            var autocars = db.autocars.Include(a => a.societe);
            return View(autocars.ToList());
        }

        // GET: autocars/Details/5
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            autocar autocar = db.autocars.Find(id);
            if (autocar == null)
            {
                return HttpNotFound();
            }
            return View(autocar);
        }

        // GET: autocars/Create
        [Route("ajouter")]
        public ActionResult Create()
        {
            ViewBag.id_ste = new SelectList(db.societes, "id", "nom_s");
            return View();
        }

        // POST: autocars/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ajouter")]
        public ActionResult Create([Bind(Include = "id,libele,marque,wifi,Nbr_place,id_ste")] autocar autocar , HttpPostedFileBase _image)
        {
            if (ModelState.IsValid)
            {
                if (_image != null)
                {
                    //recuperer le nom sans extension 
                    string FileName = Path.GetFileNameWithoutExtension(_image.FileName);
                    //Get File Extension  
                    string FileExtension = Path.GetExtension(_image.FileName);
                    //Add Current Date To Attached File Name  
                    FileName = FileName.Trim() + "_" + DateTime.Now.ToString("dd-mm-ss")
                           + FileExtension;

                    //indiquer le chemin  
                    string UploadPath = "~/images/";
                    //indiquer le chemin au niveau de la table 
                    autocar.img = FileName; //UploadPath +
                                            //save file into server.  
                    _image.SaveAs(Server.MapPath(UploadPath + FileName));
                }
               
                db.autocars.Add(autocar);
                db.SaveChanges();
                return Redirect("/trajet/choisir");
                
            }

            ViewBag.id_ste = new SelectList(db.societes, "id", "nom_s", autocar.id_ste);
            return View(autocar);
        }

        // GET: autocars/Edit/5
        [Route("modifier/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            autocar autocar = db.autocars.Find(id);
            if (autocar == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_ste = new SelectList(db.societes, "id", "nom_s", autocar.id_ste);
            return View(autocar);
        }

        // POST: autocars/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("modifier/{id}")]
        public ActionResult Edit([Bind(Include = "id,libele,marque,wifi,Nbr_place,img,id_ste")] autocar autocar , HttpPostedFileBase _image)
        {
            if (ModelState.IsValid)
            {
                if (_image != null)
                {
                    //recuperer le nom sans extension 
                    string FileName = Path.GetFileNameWithoutExtension(_image.FileName);
                    //Get File Extension  
                    string FileExtension = Path.GetExtension(_image.FileName);
                    //Add Current Date To Attached File Name  
                    FileName = FileName.Trim() + "_" + DateTime.Now.ToString("dd-mm-ss")
                           + FileExtension;

                    //indiquer le chemin  
                    string UploadPath = "~/images/";
                    //indiquer le chemin au niveau de la table 
                    autocar.img = FileName; //UploadPath +
                                            //save file into server.  
                    _image.SaveAs(Server.MapPath(UploadPath + FileName));
                }
                db.Entry(autocar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_ste = new SelectList(db.societes, "id", "nom_s", autocar.id_ste);
            return View(autocar);
        }

        // GET: autocars/Delete/5
        [Route("supprimer/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            autocar autocar = db.autocars.Find(id);
            if (autocar == null)
            {
                return HttpNotFound();
            }
            return View(autocar);
        }

        // POST: autocars/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("supprimer/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            autocar autocar = db.autocars.Find(id);
            db.autocars.Remove(autocar);
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
