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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.IO;

namespace Graduation_Projects_System.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private ApplicationUserManager _userManager;
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
            string xid = User.Identity.GetUserId();
            var applicationUsers = db.Users.Include(a => a.Department).Include(a => a.leader).Where( a => a.UserType == "Student");
            applicationUsers.ToList();
            return View(applicationUsers);
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

        

        // POST: /Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentViewModel model, HttpPostedFileBase upload)
        {
            string xid = User.Identity.GetUserId();
            ViewBag.Departmentid = new SelectList(db.Departments, "id", "name");

            if (ModelState.IsValid)
            {
                string UserType = "Student";
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, UserType = UserType, name = model.name, Departmentid = model.Departmentid, PhoneNumber = model.phone, leaderid = xid };
                var result = await UserManager.CreateAsync(user, "s123456@S");

                

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, UserType);
                    
                    Student s = new Student();

                    s.userId = user.Id;
                    s.GPA = model.GPA;
                    s.skills = model.skills;
                    s.level = model.level;
                    s.file = path;

                    db.Students.Add(s);
                    db.SaveChanges();

                    return RedirectToAction("index", "Students");
                }

                ModelState.AddModelError("", "error result");
                AddErrors(result);
            }

            ModelState.AddModelError("", "error model state");
            return View(model);
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

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
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
