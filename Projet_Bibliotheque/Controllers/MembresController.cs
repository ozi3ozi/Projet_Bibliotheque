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
    public class MembresController : Controller
    {
        private SkyBiblioEntities db = new SkyBiblioEntities();

        // GET: Membres
        public ActionResult Index()
        {
            return View(db.Membres.ToList());
        }

        // GET: Membres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membres membres = db.Membres.Find(id);
            if (membres == null)
            {
                return HttpNotFound();
            }
            return View(membres);
        }

        // GET: Membres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Membres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Membres,Nom,Prenom,Email,Phone")] Membres membres)
        {
            if (ModelState.IsValid)
            {
                db.Membres.Add(membres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(membres);
        }

        // GET: Membres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membres membres = db.Membres.Find(id);
            if (membres == null)
            {
                return HttpNotFound();
            }
            return View(membres);
        }

        // POST: Membres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Membres,Nom,Prenom,Email,Phone")] Membres membres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(membres);
        }

        // GET: Membres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membres membres = db.Membres.Find(id);
            if (membres == null)
            {
                return HttpNotFound();
            }
            return View(membres);
        }

        // POST: Membres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Membres membres = db.Membres.Find(id);
            db.Membres.Remove(membres);
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
