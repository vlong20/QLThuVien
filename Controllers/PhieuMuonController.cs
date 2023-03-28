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
    public class PhieuMuonController : Controller
    {
        private thuvienEntities db = new thuvienEntities();

        // GET: PhieuMuon
        public async Task<ActionResult> Index()
        {
            var phieuMuons = db.PhieuMuons.Include(p => p.LuaChon1).Include(p => p.NhanVien).Include(p => p.TheThuVien).Include(p => p.TrangThai1);
            return View(await phieuMuons.ToListAsync());
        }

        // GET: PhieuMuon/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = await db.PhieuMuons.FindAsync(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuon);
        }

        // GET: PhieuMuon/Create
        public ActionResult Create()
        {
            ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name");
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TaiKhoan");
            ViewBag.MaDG = new SelectList(db.TheThuViens, "MaDG", "MaDG");
            ViewBag.TrangThai = new SelectList(db.TrangThais, "id", "name");
            return View();
        }

        // POST: PhieuMuon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaPM,MaDG,MaNV,NgayMuon,NgayTra,LuaChon,TrangThai,ChiTietPhieuMuons")] PhieuMuon phieuMuon)
        {
            if (ModelState.IsValid)
            {
                
                db.PhieuMuons.Add(phieuMuon);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TaiKhoan", phieuMuon.MaNV);
            ViewBag.MaDG = new SelectList(db.TheThuViens, "MaDG", "MaDG", phieuMuon.MaDG);
            ViewBag.TrangThai = new SelectList(db.TrangThais, "id", "name", phieuMuon.TrangThai);
            return View(phieuMuon);
        }

        // GET: PhieuMuon/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = await db.PhieuMuons.FindAsync(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TaiKhoan", phieuMuon.MaNV);
            ViewBag.MaDG = new SelectList(db.TheThuViens, "MaDG", "MaDG", phieuMuon.MaDG);
            ViewBag.TrangThai = new SelectList(db.TrangThais, "id", "name", phieuMuon.TrangThai);
            return View(phieuMuon);
        }

        // POST: PhieuMuon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaPM,MaDG,MaNV,NgayMuon,NgayTra,LuaChon,TrangThai,ChiTietPhieuMuons")] PhieuMuon phieuMuon)
        {
           
            if (ModelState.IsValid)
            {
                db.Entry(phieuMuon).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TaiKhoan", phieuMuon.MaNV);
            ViewBag.MaDG = new SelectList(db.TheThuViens, "MaDG", "MaDG", phieuMuon.MaDG);
            ViewBag.TrangThai = new SelectList(db.TrangThais, "id", "name", phieuMuon.TrangThai);
            return View(phieuMuon);
        }

        // GET: PhieuMuon/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = await db.PhieuMuons.FindAsync(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuon);
        }

        // POST: PhieuMuon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PhieuMuon phieuMuon = await db.PhieuMuons.FindAsync(id);
            db.PhieuMuons.Remove(phieuMuon);
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
