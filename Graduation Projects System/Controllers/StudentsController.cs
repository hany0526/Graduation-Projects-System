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

namespace Graduation_Projects_System.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AddIdea()
        {
            // ViewBag.UserType = new SelectList(db.Roles.Where(a => a.Name.Contains("Team Leader")).ToList(), "Name", "Name");
            ViewBag.professor1id = new SelectList(db.Users.Where(p => p.UserType == "Professor"), "Id", "name");
            ViewBag.professor2id = new SelectList(db.Users.Where(p => p.UserType == "Professor"), "Id", "name");
            ViewBag.professor3id = new SelectList(db.Users.Where(p => p.UserType == "Professor"), "Id", "name");
            return View();
        }

        // POST: Ideas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIdea([Bind(Include = "id,name,description,tools,professor1id,professor2id,professor3id")] Idea idea)
        {
            if (ModelState.IsValid)
            {
                idea.isApproved = 0;
                idea.leaderid = User.Identity.GetUserId();
                db.Ideas.Add(idea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.professor1id = new SelectList(db.Users.Where(p => p.UserType == "Professor"), "Id", "name");
            ViewBag.professor2id = new SelectList(db.Users.Where(p => p.UserType == "Professor"), "Id", "name");
            ViewBag.professor3id = new SelectList(db.Users.Where(p => p.UserType == "Professor"), "Id", "name");
            return View(idea);
        }

        // GET: Students
        public ActionResult Index()
        {
            var applicationUsers = db.Users.Include(a => a.Department).Include(a => a.leader);
            return View(applicationUsers.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.Departmentid = new SelectList(db.Departments, "id", "name");
            ViewBag.leaderid = new SelectList(db.Users, "Id", "UserType");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserType,name,Departmentid,leaderid,file,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departmentid = new SelectList(db.Departments, "id", "name", applicationUser.Departmentid);
            ViewBag.leaderid = new SelectList(db.Users, "Id", "UserType", applicationUser.leaderid);
            return View(applicationUser);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.Departmentid = new SelectList(db.Departments, "id", "name", applicationUser.Departmentid);
            ViewBag.leaderid = new SelectList(db.Users, "Id", "UserType", applicationUser.leaderid);
            return View(applicationUser);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserType,name,Departmentid,leaderid,file,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departmentid = new SelectList(db.Departments, "id", "name", applicationUser.Departmentid);
            ViewBag.leaderid = new SelectList(db.Users, "Id", "UserType", applicationUser.leaderid);
            return View(applicationUser);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
