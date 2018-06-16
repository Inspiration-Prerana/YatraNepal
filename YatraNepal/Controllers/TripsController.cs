using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YatraNepal.Models;

namespace YatraNepal.Controllers
{
    public class TripsController : Controller
    {
        private YatraAdo db = new YatraAdo();

        // GET: Trips
        public ActionResult Index()
        {
            var trip = db.Trip.Include(t => t.Bus).Include(t => t.EndLocation).Include(t => t.StartLocation);
            return View(trip.ToList());
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            ViewBag.BID = new SelectList(db.Vehicle, "ID", "VehicleId");
            ViewBag.EndID = new SelectList(db.Location, "ID", "strAddress");
            ViewBag.StartID = new SelectList(db.Location, "ID", "strAddress");
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BID,StartID,EndID,time,price")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Trip.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BID = new SelectList(db.Vehicle, "ID", "VehicleId", trip.BID);
            ViewBag.EndID = new SelectList(db.Location, "ID", "strAddress", trip.EndID);
            ViewBag.StartID = new SelectList(db.Location, "ID", "strAddress", trip.StartID);
            return View(trip);
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            ViewBag.BID = new SelectList(db.Vehicle, "ID", "VehicleId", trip.BID);
            ViewBag.EndID = new SelectList(db.Location, "ID", "strAddress", trip.EndID);
            ViewBag.StartID = new SelectList(db.Location, "ID", "strAddress", trip.StartID);
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BID,StartID,EndID,time,price")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BID = new SelectList(db.Vehicle, "ID", "VehicleId", trip.BID);
            ViewBag.EndID = new SelectList(db.Location, "ID", "strAddress", trip.EndID);
            ViewBag.StartID = new SelectList(db.Location, "ID", "strAddress", trip.StartID);
            return View(trip);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trip.Find(id);
            db.Trip.Remove(trip);
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
