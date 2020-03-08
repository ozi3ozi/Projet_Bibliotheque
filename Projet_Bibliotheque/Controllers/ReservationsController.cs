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
    public class ReservationsController : Controller
    {
        private SkyBiblioEntities db = new SkyBiblioEntities();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservation = db.Reservation.Include(r => r.Livres).Include(r => r.Membres);
            return View(reservation.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.Id_Livres = new SelectList(db.Livres, "Id_Livres", "Titre");
            ViewBag.Id_Membres = new SelectList(db.Membres, "Id_Membres", "Nom");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Reserv,Id_Livres,Id_Membres")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservation.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Livres = new SelectList(db.Livres, "Id_Livres", "Titre", reservation.Id_Livres);
            ViewBag.Id_Membres = new SelectList(db.Membres, "Id_Membres", "Nom", reservation.Id_Membres);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Livres = new SelectList(db.Livres, "Id_Livres", "Titre", reservation.Id_Livres);
            ViewBag.Id_Membres = new SelectList(db.Membres, "Id_Membres", "Nom", reservation.Id_Membres);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Reserv,Id_Livres,Id_Membres")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Livres = new SelectList(db.Livres, "Id_Livres", "Titre", reservation.Id_Livres);
            ViewBag.Id_Membres = new SelectList(db.Membres, "Id_Membres", "Nom", reservation.Id_Membres);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservation.Find(id);
            db.Reservation.Remove(reservation);
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
