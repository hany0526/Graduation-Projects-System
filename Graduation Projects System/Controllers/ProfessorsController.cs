using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Graduation_Projects_System.Models;
using Microsoft.AspNet.Identity;

namespace GP.Controllers
{
    public class ProfessorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Professors
        public ActionResult Index()
        {
            return View(db.Professors.ToList());
        }

        // GET: Professors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // GET: Professors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,email,password")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Professors.Add(professor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(professor);
        }

        // GET: Professors/Edit/5
        public ActionResult Edit()
        {
            ViewBag.Departmentid = new SelectList(db.Departments, "id", "name");
            string id = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser u = db.Users.Include(a => a.Department).Where(a => a.Id == id).FirstOrDefault();

            if (u == null)
            {
                return HttpNotFound();
            }
            ViewBag.Departmentid = new SelectList(db.Departments, "id", "name", u.Departmentid);

            ProfessorEditProfileViewModel p = new ProfessorEditProfileViewModel()
            {
                
                departmentid = u.Departmentid,
                department = u.Department,
                name = u.name,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
                
            };

            return View(p);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfessorEditProfileViewModel professor)
        {
            ViewBag.Departmentid = new SelectList(db.Departments, "id", "name");
            if (ModelState.IsValid)
            {
                string xid = User.Identity.GetUserId();

                ApplicationUser appuser = db.Users.Find(xid);
                appuser.name = professor.name;
                appuser.Email = professor.Email;
                appuser.UserName = professor.Email;
                db.SaveChanges();

                Professor i = db.Professors.FirstOrDefault(a => a.userId == xid);

                i.Interests = professor.Interests;
                i.IsAproved = 1;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Invalid Edit Profile.");
            return View(professor);
        }

        // GET: Professors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Professor professor = db.Professors.Find(id);
            db.Professors.Remove(professor);
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
