using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hospitalwebapp.Models;

namespace hospitalwebapp.Controllers
{
    public class StaffAccountsController : Controller
    {
        private HospitalDbContext db = new HospitalDbContext();

        // GET: StaffAccounts
        public ActionResult Index()
        {
            return View(db.StaffAccounts.ToList());
        }

        // GET: StaffAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAccount staffAccount = db.StaffAccounts.Find(id);
            if (staffAccount == null)
            {
                return HttpNotFound();
            }
            return View(staffAccount);
        }

        // GET: StaffAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffId,FirstName,LastName,StaffGender,Profession,StaffDepartment,DateOfBirth,Email,Address,Phone,Username,Password,ConfirmPassword")] StaffAccount staffAccount)
        {
            if (ModelState.IsValid)
            {
                db.StaffAccounts.Add(staffAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffAccount);
        }

        // GET: StaffAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAccount staffAccount = db.StaffAccounts.Find(id);
            if (staffAccount == null)
            {
                return HttpNotFound();
            }
            return View(staffAccount);
        }

        // POST: StaffAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffId,FirstName,LastName,StaffGender,Profession,StaffDepartment,DateOfBirth,Email,Address,Phone,Username,Password,ConfirmPassword")] StaffAccount staffAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staffAccount);
        }

        // GET: StaffAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffAccount staffAccount = db.StaffAccounts.Find(id);
            if (staffAccount == null)
            {
                return HttpNotFound();
            }
            return View(staffAccount);
        }

        // POST: StaffAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffAccount staffAccount = db.StaffAccounts.Find(id);
            db.StaffAccounts.Remove(staffAccount);
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
