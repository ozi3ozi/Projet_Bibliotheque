using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projet_Bibliotheque.Models;

namespace Projet_Bibliotheque.Controllers
{
    public class PretsController : Controller
    {
        private SkyBiblioEntities db = new SkyBiblioEntities();

        // GET: Prets
        public ActionResult Index()
        {
            var prets = db.Prets.Include(p => p.Livres).Include(p => p.Membres);
            return View(prets.ToList());
        }

        // GET: Prets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prets prets = db.Prets.Find(id);
            if (prets == null)
            {
                return HttpNotFound();
            }
            return View(prets);
        }

        // GET: Prets/Create
        public ActionResult Create()
        {
            ViewBag.Id_Livres = new SelectList(db.Livres, "Id_Livres", "Titre");
            ViewBag.Id_Membres = new SelectList(db.Membres, "Id_Membres", "Nom");
            return View();
        }

        // POST: Prets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Prets,Id_Livres,Id_Membres,date_de_Pret,date_de_retour")] Prets prets)
        {

            var rent = from a in db.Prets where a.Id_Membres == prets.Id_Membres select a;
            int count = rent.Count();



            
                Livres b = db.Livres.Find(prets.Id_Livres);
                if (ModelState.IsValid)
                {
                    if (count < 3)
                    {
                        if (b.Quantite <= 0)
                        {


                            ViewBag.error = "Le livres n'existe pas!";


                        }
                        else
                        {
                            db.Prets.Add(prets);
                            db.SaveChanges();


                            return RedirectToAction("Index");
                        }

                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Vous n'avez pas le droit de plus de 3 livres";
                    }
                }
            

                ViewBag.Id_Livres = new SelectList(db.Livres, "Id_Livres", "Titre", prets.Id_Livres);
                ViewBag.Id_Membres = new SelectList(db.Membres, "Id_Membres", "Nom", prets.Id_Membres);
                return View(prets);
            
        }

        // GET: Prets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prets prets = db.Prets.Find(id);
            if (prets == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Livres = new SelectList(db.Livres, "Id_Livres", "Titre", prets.Id_Livres);
            ViewBag.Id_Membres = new SelectList(db.Membres, "Id_Membres", "Nom", prets.Id_Membres);
            return View(prets);
        }

        // POST: Prets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Prets,Id_Livres,Id_Membres,date_de_Pret,date_de_retour")] Prets prets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Livres = new SelectList(db.Livres, "Id_Livres", "Titre", prets.Id_Livres);
            ViewBag.Id_Membres = new SelectList(db.Membres, "Id_Membres", "Nom", prets.Id_Membres);
            return View(prets);
        }

        // GET: Prets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prets prets = db.Prets.Find(id);
            if (prets == null)
            {
                return HttpNotFound();
            }
            return View(prets);
        }

        // POST: Prets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prets prets = db.Prets.Find(id);
            db.Prets.Remove(prets);
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
