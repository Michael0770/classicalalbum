using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ass3.Models;

namespace ass3.Controllers
{
    public class UsersController : Controller
    {
        private AmigosDBEntities db = new AmigosDBEntities();

        // GET: Users
        public ActionResult Index(string searchString)
        {
            var users = from u in db.Users.Include(u => u.States).Include(u => u.Albums) select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.firstName.ToUpper().Contains(searchString.ToUpper())
                    || s.lastName.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(users.OrderBy(p=>p.lastName).ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.state = new SelectList(db.States, "stateID", "stateCode");
            ViewBag.album = new SelectList(db.Albums, "albumID", "albumName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,firstName,lastName,DOB,state,postcode,album")] Users users)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(users);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Please make sure you enter a valid postcode");
            }


            ViewBag.state = new SelectList(db.States, "stateID", "stateCode", users.state);
            ViewBag.album = new SelectList(db.Albums, "albumID", "albumName", users.album);
            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.state = new SelectList(db.States, "stateID", "stateCode", users.state);
            ViewBag.album = new SelectList(db.Albums, "albumID", "albumName", users.album);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,firstName,lastName,DOB,state,postcode,album")] Users users)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(users).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Please make sure you enter a valid postcode");
            }
            
            ViewBag.state = new SelectList(db.States, "stateID", "stateCode", users.state);
            ViewBag.album = new SelectList(db.Albums, "albumID", "albumName", users.album);
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
