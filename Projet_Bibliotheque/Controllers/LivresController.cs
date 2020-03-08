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
    public class LivresController : Controller
    {
        private SkyBiblioEntities db = new SkyBiblioEntities();

        // GET: Livres
        public ActionResult Index()
        {
            return View(db.Livres.ToList());
        }

        // GET: Livres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livres livres = db.Livres.Find(id);
            if (livres == null)
            {
                return HttpNotFound();
            }
            return View(livres);
        }

        // GET: Livres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Livres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Livres,Titre,Auteur,Categorie,Quantite")] Livres livres)
        {
            if (ModelState.IsValid)
            {
                db.Livres.Add(livres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(livres);
        }

        // GET: Livres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livres livres = db.Livres.Find(id);
            if (livres == null)
            {
                return HttpNotFound();
            }
            return View(livres);
        }

        // POST: Livres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Livres,Titre,Auteur,Categorie,Quantite")] Livres livres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(livres);
        }

        // GET: Livres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livres livres = db.Livres.Find(id);
            if (livres == null)
            {
                return HttpNotFound();
            }
            return View(livres);
        }

        // POST: Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livres livres = db.Livres.Find(id);
            db.Livres.Remove(livres);
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
