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
    public class AdminInfoesController : Controller
    {
        private YatraAdo db = new YatraAdo();

        // GET: AdminInfoes
        public ActionResult Index()
        {
            return View(db.Admin.ToList());
        }

        // GET: AdminInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminInfo adminInfo = db.Admin.Find(id);
            if (adminInfo == null)
            {
                return HttpNotFound();
            }
            return View(adminInfo);
        }

        // GET: AdminInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AgentId,FName,LName,Address,PhoneNumber,RegisteredDate,Gender,Birthday,Age")] AdminInfo adminInfo)
        {
            if (ModelState.IsValid)
            {
                adminInfo.AdminId = GetCurrentUserId();
                adminInfo.RegisteredDate = DateTime.Now;
                adminInfo.Age = GetAge(adminInfo);
                db.Admin.Add(adminInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminInfo);
        }

        // GET: AdminInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminInfo adminInfo = db.Admin.Find(id);
            if (adminInfo == null)
            {
                return HttpNotFound();
            }
            return View(adminInfo);
        }

        // POST: AdminInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AgentId,FName,LName,Address,PhoneNumber,RegisteredDate,Gender,Birthday,Age")] AdminInfo adminInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminInfo);
        }

        // GET: AdminInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminInfo adminInfo = db.Admin.Find(id);
            if (adminInfo == null)
            {
                return HttpNotFound();
            }
            return View(adminInfo);
        }

        // POST: AdminInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminInfo adminInfo = db.Admin.Find(id);
            db.Admin.Remove(adminInfo);
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
        public Guid GetCurrentUserId()
        { //to allow user to view only their details
            return new Guid(User.Identity.GetUserId());
        }
        public bool EnsureIsUserContact(AdminInfo adminInfo)
        {
            return adminInfo.AdminId== GetCurrentUserId();
        }
        public int GetAge(AdminInfo adminInfo)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - adminInfo.Birthday.Year;
            // Go back to the year the person was born in case of a leap year
            if (adminInfo.Birthday > today.AddYears(-age)) age--;
            return age;
        }
    }
}
