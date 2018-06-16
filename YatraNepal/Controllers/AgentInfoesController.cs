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
    public class AgentInfoesController : Controller
    {
        private YatraAdo db = new YatraAdo();

        // GET: AgentInfoes
        public ActionResult Index()
        {
            return View(db.Agent.ToList());
        }
        [Authorize(Roles ="Agent")] // only agent can create agents
        public ActionResult AgentWall()
        {
            return View();
        }
        // GET: AgentInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentInfo agentInfo = db.Agent.Find(id);
            if (agentInfo == null)
            {
                return HttpNotFound();
            }
            return View(agentInfo);
        }
        [Authorize(Roles = "Admin")] // only agent can create agents

        // GET: AgentInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgentInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AgentId,FName,LName,Address,PhoneNumber,RegisteredDate,Gender,Birthday,Age")] AgentInfo agentInfo)
        {
            if (ModelState.IsValid)
            {
                agentInfo.AgentId = GetCurrentUserId();
                agentInfo.RegisteredDate = DateTime.Now;
                agentInfo.Age = GetAge(agentInfo);
                db.Agent.Add(agentInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agentInfo);
        }

        // GET: AgentInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentInfo agentInfo = db.Agent.Find(id);
            if (agentInfo == null)
            {
                return HttpNotFound();
            }
            return View(agentInfo);
        }

        // POST: AgentInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AgentId,FName,LName,Address,PhoneNumber,RegisteredDate,Gender,Birthday,Age")] AgentInfo agentInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agentInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentInfo);
        }

        // GET: AgentInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentInfo agentInfo = db.Agent.Find(id);
            if (agentInfo == null)
            {
                return HttpNotFound();
            }
            return View(agentInfo);
        }

        // POST: AgentInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgentInfo agentInfo = db.Agent.Find(id);
            db.Agent.Remove(agentInfo);
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
        public bool EnsureIsUserContact(AgentInfo agentInfo)
        {
            return agentInfo.AgentId == GetCurrentUserId();
        }
        public int GetAge(AgentInfo agentInfo)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - agentInfo.Birthday.Year;
            // Go back to the year the person was born in case of a leap year
            if (agentInfo.Birthday > today.AddYears(-age)) age--;
            return age;
        }
    }
}
