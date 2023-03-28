using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using THPTPhuBinh.Models;
using System.Web.WebPages;

namespace THPTPhuBinh.Controllers
{
    public class TaiKhoanController : Controller
    {
        private thuvienEntities db = new thuvienEntities();

        // GET: TaiKhoan
        public async Task<ActionResult> Index(string searchString)
        {
            var taiKhoans = db.TaiKhoans.Include(t => t.LoaiTK1);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                taiKhoans = db.TaiKhoans.Where(b => b.TaiKhoan1.ToLower().Contains(searchString));
                
            }
            return View(await taiKhoans.ToListAsync());
        }

        // GET: TaiKhoan/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = await db.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: TaiKhoan/Create
        public ActionResult Create()
        {
            ViewBag.LoaiTK = new SelectList(db.LoaiTKs, "id", "name");
            return View();
        }

        // POST: TaiKhoan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TaiKhoan1,MatKhau,LoaiTK")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(taiKhoan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiTK = new SelectList(db.LoaiTKs, "id", "name", taiKhoan.LoaiTK);
            return View(taiKhoan);
        }

        // GET: TaiKhoan/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = await db.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiTK = new SelectList(db.LoaiTKs, "id", "name", taiKhoan.LoaiTK);
            return View(taiKhoan);
        }

        // POST: TaiKhoan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TaiKhoan1,MatKhau,LoaiTK")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiTK = new SelectList(db.LoaiTKs, "id", "name", taiKhoan.LoaiTK);
            return View(taiKhoan);
        }

        // GET: TaiKhoan/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = await db.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: TaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            TaiKhoan taiKhoan = await db.TaiKhoans.FindAsync(id);
            db.TaiKhoans.Remove(taiKhoan);
            await db.SaveChangesAsync();
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
