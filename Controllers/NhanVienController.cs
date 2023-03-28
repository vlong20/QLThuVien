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

namespace THPTPhuBinh.Controllers.Admin
{
    public class NhanVienController : Controller
    {
        private thuvienEntities db = new thuvienEntities();

        // GET: NhanVien
        public async Task<ActionResult> Index(string searchString)
        {
            var nhanViens = db.NhanViens.Include(n => n.Nguoi).Include(n => n.TaiKhoan1);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                var nhanvien = db.NhanViens.Where(b => b.Nguoi.HoTen.ToLower().Contains(searchString));
                return View(nhanvien.ToList());
            }
            return View(await nhanViens.ToListAsync());
        }

        // GET: NhanVien/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = await db.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Create
        public ActionResult Create()
        {
            
            
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaNV,Nguoi,TaiKhoan1")] NhanVien nhanVien)
        {
            nhanVien.MaNguoi = nhanVien.Nguoi.MaNguoi;
            nhanVien.TaiKhoan = nhanVien.TaiKhoan1.TaiKhoan1;
            if (ModelState.IsValid && db.NhanViens.Find(nhanVien.MaNV) == null && db.TaiKhoans.Find(nhanVien.TaiKhoan) == null)
            {
                Nguoi nguoi = nhanVien.Nguoi;
                TaiKhoan taikhoan = nhanVien.TaiKhoan1;

                db.Nguois.Add(nguoi);
                await db.SaveChangesAsync();

                db.TaiKhoans.Add(taikhoan);
                await db.SaveChangesAsync();

                db.NhanViens.Add(nhanVien);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            
            return View(nhanVien);
        }

        // GET: NhanVien/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = await db.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNguoi = new SelectList(db.Nguois, "MaNguoi", "HoTen", nhanVien.MaNguoi);
            ViewBag.TaiKhoan = new SelectList(db.TaiKhoans, "TaiKhoan1", "MatKhau", nhanVien.TaiKhoan);
            return View(nhanVien);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaNV,MaNguoi,TaiKhoan")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaNguoi = new SelectList(db.Nguois, "MaNguoi", "HoTen", nhanVien.MaNguoi);
            ViewBag.TaiKhoan = new SelectList(db.TaiKhoans, "TaiKhoan1", "MatKhau", nhanVien.TaiKhoan);
            return View(nhanVien);
        }

        // GET: NhanVien/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = await db.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            NhanVien nhanVien = await db.NhanViens.FindAsync(id);
            db.NhanViens.Remove(nhanVien);
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
