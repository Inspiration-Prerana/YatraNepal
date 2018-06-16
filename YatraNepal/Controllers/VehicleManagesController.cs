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
    public class VehicleManagesController : Controller
    {
        private YatraAdo db = new YatraAdo();

        // GET: VehicleManages
        public ActionResult Index()
        {
            var vManage = db.VManage.Include(v => v.Bus);
            return View(vManage.ToList());
        }

        // GET: VehicleManages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleManage vehicleManage = db.VManage.Find(id);
            if (vehicleManage == null)
            {
                return HttpNotFound();
            }
            return View(vehicleManage);
        }

        // GET: VehicleManages/Create
        public ActionResult Create()
        {
            ViewBag.BID = new SelectList(db.Vehicle, "ID", "VehicleId");
            return View();
        }

        // POST: VehicleManages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AgentID,BID")] VehicleManage vehicleManage)
        {
            if (ModelState.IsValid)
            {
                db.VManage.Add(vehicleManage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BID = new SelectList(db.Vehicle, "ID", "VehicleId", vehicleManage.BID);
            return View(vehicleManage);
        }

        // GET: VehicleManages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleManage vehicleManage = db.VManage.Find(id);
            if (vehicleManage == null)
            {
                return HttpNotFound();
            }
            ViewBag.BID = new SelectList(db.Vehicle, "ID", "VehicleId", vehicleManage.BID);
            return View(vehicleManage);
        }

        // POST: VehicleManages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AgentID,BID")] VehicleManage vehicleManage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleManage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BID = new SelectList(db.Vehicle, "ID", "VehicleId", vehicleManage.BID);
            return View(vehicleManage);
        }

        // GET: VehicleManages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleManage vehicleManage = db.VManage.Find(id);
            if (vehicleManage == null)
            {
                return HttpNotFound();
            }
            return View(vehicleManage);
        }

        // POST: VehicleManages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleManage vehicleManage = db.VManage.Find(id);
            db.VManage.Remove(vehicleManage);
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
