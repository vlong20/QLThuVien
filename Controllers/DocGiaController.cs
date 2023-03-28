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

namespace THPTPhuBinh.Controllers
{
    public class DocGiaController : Controller
    {
        private thuvienEntities db = new thuvienEntities();

        // GET: DocGia
        public async Task<ActionResult> Index(string searchString)
        {
            var docGias = db.DocGias.Include(d => d.LoaiDG1).Include(d => d.TheThuVien).Include(d => d.Nguoi);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString= searchString.ToLower();
                docGias = docGias.Where(x => x.Nguoi.HoTen.ToLower().Contains(searchString));

            }
            return View(await docGias.ToListAsync());
        }

        // GET: DocGia/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocGia docGia = await db.DocGias.FindAsync(id);
            if (docGia == null)
            {
                return HttpNotFound();
            }
            return View(docGia);
        }

        // GET: DocGia/Create
        public ActionResult Create()
        {
            ViewBag.LoaiDG = new SelectList(db.LoaiDGs, "id", "name");
            return View();
        }

        // POST: DocGia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaDG,NgheNghiep,LoaiDG,Nguoi,TheThuVien")] DocGia docGia)
        {
            docGia.MaNguoi = docGia.Nguoi.MaNguoi;
            

            
            if (ModelState.IsValid)
            {
                Nguoi nguoi = docGia.Nguoi;
                db.Nguois.Add(nguoi);
                await db.SaveChangesAsync();

                db.DocGias.Add(docGia);
                await db.SaveChangesAsync();

                docGia.TheThuVien.MaDG = docGia.MaDG;
                TheThuVien theThuVien = docGia.TheThuVien;
                db.TheThuViens.Add(theThuVien);
                try
                {
                    await db.SaveChangesAsync();
                }
                catch {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");


            }

            ViewBag.LoaiDG = new SelectList(db.LoaiDGs, "id", "name", docGia.LoaiDG);
            ViewBag.MaDG = new SelectList(db.TheThuViens, "MaDG", "MaDG", docGia.MaDG);
            ViewBag.MaNguoi = new SelectList(db.Nguois, "MaNguoi", "HoTen", docGia.MaNguoi);
            return View(docGia);
        }

        // GET: DocGia/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocGia docGia = await db.DocGias.FindAsync(id);
            if (docGia == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiDG = new SelectList(db.LoaiDGs, "id", "name", docGia.LoaiDG);
            ViewBag.MaDG = new SelectList(db.TheThuViens, "MaDG", "MaDG", docGia.MaDG);
            ViewBag.MaNguoi = new SelectList(db.Nguois, "MaNguoi", "HoTen", docGia.MaNguoi);
            return View(docGia);
        }

        // POST: DocGia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaDG,NgheNghiep,LoaiDG,MaNguoi")] DocGia docGia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docGia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiDG = new SelectList(db.LoaiDGs, "id", "name", docGia.LoaiDG);
            ViewBag.MaDG = new SelectList(db.TheThuViens, "MaDG", "MaDG", docGia.MaDG);
            ViewBag.MaNguoi = new SelectList(db.Nguois, "MaNguoi", "HoTen", docGia.MaNguoi);
            return View(docGia);
        }

        // GET: DocGia/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocGia docGia = await db.DocGias.FindAsync(id);
            if (docGia == null)
            {
                return HttpNotFound();
            }
            return View(docGia);
        }

        // POST: DocGia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TheThuVien theThuVien = await db.TheThuViens.FindAsync(id);
            db.TheThuViens.Remove(theThuVien);
            await db.SaveChangesAsync();

            
            DocGia docGia = await db.DocGias.FindAsync(id);
            int manguoi = docGia.Nguoi.MaNguoi;
            db.DocGias.Remove(docGia);
            await db.SaveChangesAsync();

            Nguoi nguoi = await db.Nguois.FindAsync(manguoi);
            db.Nguois.Remove(nguoi);
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
