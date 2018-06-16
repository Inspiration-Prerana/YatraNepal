using Microsoft.AspNet.Identity;
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
    public class TicketsController : Controller
    {
        private YatraAdo db = new YatraAdo();

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.EndLocation).Include(t => t.StartLocation).Include(t => t.Trip);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.EndID = new SelectList(db.Location, "ID", "strAddress");
            ViewBag.StartID = new SelectList(db.Location, "ID", "strAddress");
            ViewBag.TripID = new SelectList(db.Trip, "ID", "ID");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,TripID,StartID,EndID,SeatNo,Price")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.UserID = new Guid(User.Identity.GetUserId());
                ticket.ID = Guid.NewGuid();
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EndID = new SelectList(db.Location, "ID", "strAddress", ticket.EndID);
            ViewBag.StartID = new SelectList(db.Location, "ID", "strAddress", ticket.StartID);
            ViewBag.TripID = new SelectList(db.Trip, "ID", "ID", ticket.TripID);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.EndID = new SelectList(db.Location, "ID", "strAddress", ticket.EndID);
            ViewBag.StartID = new SelectList(db.Location, "ID", "strAddress", ticket.StartID);
            ViewBag.TripID = new SelectList(db.Trip, "ID", "ID", ticket.TripID);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,TripID,StartID,EndID,SeatNo,Price")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EndID = new SelectList(db.Location, "ID", "strAddress", ticket.EndID);
            ViewBag.StartID = new SelectList(db.Location, "ID", "strAddress", ticket.StartID);
            ViewBag.TripID = new SelectList(db.Trip, "ID", "ID", ticket.TripID);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
