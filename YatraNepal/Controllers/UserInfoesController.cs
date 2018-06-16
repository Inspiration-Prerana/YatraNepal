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
    public class UserInfoesController : Controller
    {
        private YatraAdo db = new YatraAdo();
        public ActionResult UserWall()
        {
            return View();
        }
        // GET: UserInfoes
        public ActionResult Index()
        {
            return View(db.User.ToList());
        }

        // GET: UserInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.User.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserId,FName,LName,Address,PhoneNumber,RegisteredDate,Gender,Birthday,Age")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                userInfo.UserId = GetCurrentUserId();
                userInfo.RegisteredDate = DateTime.Now;
                userInfo.Age = GetAge(userInfo);
                db.User.Add(userInfo);
                db.SaveChanges();
                return RedirectToAction("Index", "Trips", userInfo);
            }

            return View(userInfo);
        }

        // GET: UserInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.User.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserId,FName,LName,Address,PhoneNumber,RegisteredDate,Gender,Birthday,Age")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.User.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInfo userInfo = db.User.Find(id);
            db.User.Remove(userInfo);
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
        public bool EnsureIsUserContact(UserInfo userInfo)
        {
            return userInfo.UserId == GetCurrentUserId();
        }
        public int GetAge(UserInfo userInfo)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - userInfo.Birthday.Year;
            // Go back to the year the person was born in case of a leap year
            if (userInfo.Birthday > today.AddYears(-age)) age--;
            return age;
        }
    }
}
