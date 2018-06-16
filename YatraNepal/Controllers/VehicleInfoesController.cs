using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YatraNepal.Models;

namespace YatraNepal.Controllers
{
    public class VehicleInfoesController : Controller
    {
        private YatraAdo db = new YatraAdo();

        string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=YatraNepal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // GET: VehicleInfoes
        public ActionResult Index()
        {
            return View(db.Vehicle.ToList());
        }

        public ActionResult MapsView()
        {
            return View();
        }


        public ActionResult RoutesView()
        {
            return View();
        }
        public ActionResult PositionView()
        {
            return View();
        }
       
        // GET: VehicleInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleInfo vehicleInfo = db.Vehicle.Find(id);
            if (vehicleInfo == null)
            {
                return HttpNotFound();
            }
            return View(vehicleInfo);
        }

        // GET: VehicleInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VehicleId,OwnerName,OwnerTel,OwnerAddr,SimNo,VehType,RegisteredDate")] VehicleInfo vehicleInfo)
        {
            if (ModelState.IsValid)
            {
                var vehicleId = vehicleInfo.VehicleId;

                try
                {
                    using (SqlCommand cmd = new SqlCommand("CREATE TABLE [dbo].[Track" + vehicleId + "]("
                                     //+ "[ID] [int] IDENTITY(1,1) NOT NULL,"
                                     //+ "[strTEID] [nvarchar](max) NOT NULL,"
                                     //+ "[nTime] [int] NOT NULL,"
                                     //+ "[dbLon] [double],"
                                     //+ "[dbLat] [double],"
                                     //+ "[nDirection] [double],"
                                     //+ "[nSpeed] [double],"
                                     //+ "[nGSMSignal] [int],"
                                     //+ "[nGPSSignal] [int],"
                                     //+ "[nFuel] [double],"
                                     //+ "[nMileage] [double],"
                                     //+ "[nTemp] [float],"
                                     //+ "[nCarState] [string],"
                                     //+ "[nTEState] [string],"
                                     //+ "[nAlarmState] [string],"
                                     + "[ID] [int] IDENTITY(1,1) NOT NULL,"
                                     + "[strTEID] [nvarchar](max) NOT NULL,"
                                    + "[nTime] [int] NOT NULL,"
                                    //+ "[dbLon] [int] NOT NULL,"
                                    //+ "[dbLat] [int] NOT NULL,"
                                    //+ "[ProductName] [nvarchar](50) NOT NULL,"
                                    //+ "[Quantity] [int] NOT NULL,"
                                    //+ "[SelfPrice] [decimal](18, 2) NOT NULL,"
                                    //+ "[Price] [decimal](18, 2) NOT NULL,"
                                    //+ "[Disccount] [int] NULL,"

                                    + "[dbLon] [NUMERIC](10, 7) NOT NULL,"
                                    + "[dbLat] [NUMERIC](10, 7) NOT NULL,"
                                    + "[nDirection] [SMALLINT] NULL,"
                                    + "[nSpeed] [TINYINT] NULL,"
                                    + "[nGSMSignal] [TINYINT] NULL,"
                                    + "[nGPSSignal] [TINYINT] NULL,"
                                    + "[nFuel] [INT] NULL,"
                                    + "[nMileage] [INT] NULL,"
                                    + "[nTemp] [SMALLINT] NULL,"
                                    + "[nCarState] [INT] NULL,"
                                    + "[Comment] [nvarchar](max) NULL,"
                                    + "CONSTRAINT [pk_" + vehicleId + "] PRIMARY KEY CLUSTERED "
                                    + "("
                                    + "[ID] ASC"
                                    + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                    + ") ON [PRIMARY]", new SqlConnection(ConnectionString)))
                    {
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                vehicleInfo.RegisteredDate = DateTime.Now;
                db.Vehicle.Add(vehicleInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleInfo);
        }

        // GET: VehicleInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleInfo vehicleInfo = db.Vehicle.Find(id);
            if (vehicleInfo == null)
            {
                return HttpNotFound();
            }
            return View(vehicleInfo);
        }

        // POST: VehicleInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VehicleId,OwnerName,OwnerTel,OwnerAddr,SimNo,VehType,RegisteredDate")] VehicleInfo vehicleInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleInfo);
        }

        // GET: VehicleInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleInfo vehicleInfo = db.Vehicle.Find(id);
            if (vehicleInfo == null)
            {
                return HttpNotFound();
            }
            return View(vehicleInfo);
        }

        // POST: VehicleInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleInfo vehicleInfo = db.Vehicle.Find(id);
            db.Vehicle.Remove(vehicleInfo);
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
