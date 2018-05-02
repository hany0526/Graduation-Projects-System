using Graduation_Projects_System.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Graduation_Projects_System.Controllers
{
    
    public class AdminController : Controller
    {
        
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ListProfessors()
        {
            var applicationUsers = db.Users.Where(a => a.UserType == "Professor").Include(a => a.Department).Include(a => a.leader);
            return View(applicationUsers.ToList());
        }

        
        public ActionResult AddProfessor()
        {
            ViewBag.Departmentid = new SelectList(db.Departments, "id", "name");
            return View();
        }
        
        // POST: /Admin/AddProfessor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddProfessor(ProfessorViewModel model)
        {
            //return RedirectToAction("ListProfessors", "Admin");
                    
            ViewBag.Departmentid = new SelectList(db.Departments, "id", "name");
            
            if (ModelState.IsValid)
            {
                string UserType = "Professor";
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, UserType = UserType, name = model.name, Departmentid = model.departmentid, PhoneNumber = model.PhoneNumber, leaderid = "0" };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {                                     
                    await UserManager.AddToRoleAsync(user.Id, UserType);
                    return RedirectToAction("ListProfessors", "Admin");
                }

                ModelState.AddModelError("", "error result");
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "error model state");
            return View(model);
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //GET Admin/ViewProfessor
        public ActionResult ViewProfessor(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser u = db.Users.Include(a => a.Department).Where(a => a.Id == id).FirstOrDefault();

            if (u == null)
            {
                return HttpNotFound();
            }
            ProfessorViewModel p = new ProfessorViewModel() { name = u.name, department = u.Department, Id = u.Id, PhoneNumber = u.PhoneNumber, };
            return View(u);
        }

        // GET: Admin/Delete/5
        public ActionResult DeleteProfessor(string id)
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
            ProfessorViewModel p = new ProfessorViewModel();
            p.Id = applicationUser.Id;
            p.name = applicationUser.name;
            p.Email = applicationUser.Email;
            p.PhoneNumber = applicationUser.PhoneNumber;
            p.department = applicationUser.Department;
            p.departmentid = applicationUser.Departmentid;

            return View(p);
        }

        // POST: Admin/DeleteProfessor/5
        [HttpPost, ActionName("DeleteProfessor")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("ListProfessors");
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
    }
}
