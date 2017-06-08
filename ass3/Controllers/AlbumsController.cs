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
    public class AlbumsController : Controller
    {
        private AmigosDBEntities db = new AmigosDBEntities();

        // GET: Albums
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Album_Desc" : "";
            ViewBag.DateSortParm = sortOrder == "Year" ? "Year_Desc" : "Year";
            var albums = from a in db.Albums select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(s => s.albumName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Album_Desc":
                    albums = albums.OrderByDescending(s => s.albumName);
                    break;
                case "Year":
                    albums = albums.OrderBy(s => s.albumYear);
                    break;
                case "Year_Desc":
                    albums = albums.OrderByDescending(s => s.albumYear);
                    break;
                default:
                    albums = albums.OrderBy(s => s.albumName);
                    break;
            }
            return View(albums.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Albums albums = db.Albums.Find(id);
            if (albums == null)
            {
                return HttpNotFound();
            }
            return View(albums);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "albumID,albumName,albumYear")] Albums albums)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Albums.Add(albums);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Please make sure the year is bigger than 1970");
            }

            return View(albums);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Albums albums = db.Albums.Find(id);
            if (albums == null)
            {
                return HttpNotFound();
            }
            return View(albums);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "albumID,albumName,albumYear")] Albums albums)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(albums).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Please make sure the year is bigger than 1970");
            }
            
            return View(albums);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Albums albums = db.Albums.Find(id);
            if (albums == null)
            {
                return HttpNotFound();
            }
            return View(albums);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Albums albums = db.Albums.Find(id);
            db.Albums.Remove(albums);
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
