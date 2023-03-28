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
    public class ChiTietPhieuMuonController : Controller
    {
        private thuvienEntities db = new thuvienEntities();

        // GET: ChiTietPhieuMuon
        public async Task<ActionResult> Index()
        {
            var chiTietPhieuMuons = db.ChiTietPhieuMuons.Include(c => c.PhieuMuon).Include(c => c.Sach);
            return View(await chiTietPhieuMuons.ToListAsync());
        }

        // GET: ChiTietPhieuMuon/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuMuon chiTietPhieuMuon = await db.ChiTietPhieuMuons.FindAsync(id);
            if (chiTietPhieuMuon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuMuon);
        }

        // GET: ChiTietPhieuMuon/Create
        public ActionResult Create()
        {
            ViewBag.MaPM = new SelectList(db.PhieuMuons, "MaPM", "MaNV");
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach");
            return View();
        }

        // POST: ChiTietPhieuMuon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaCT,MaPM,SoLuong,MaSach")] ChiTietPhieuMuon chiTietPhieuMuon)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietPhieuMuons.Add(chiTietPhieuMuon);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaPM = new SelectList(db.PhieuMuons, "MaPM", "MaNV", chiTietPhieuMuon.MaPM);
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", chiTietPhieuMuon.MaSach);
            return View(chiTietPhieuMuon);
        }

        // GET: ChiTietPhieuMuon/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuMuon chiTietPhieuMuon = await db.ChiTietPhieuMuons.FindAsync(id);
            if (chiTietPhieuMuon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPM = new SelectList(db.PhieuMuons, "MaPM", "MaNV", chiTietPhieuMuon.MaPM);
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", chiTietPhieuMuon.MaSach);
            return View(chiTietPhieuMuon);
        }

        // POST: ChiTietPhieuMuon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaCT,MaPM,SoLuong,MaSach")] ChiTietPhieuMuon chiTietPhieuMuon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietPhieuMuon).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaPM = new SelectList(db.PhieuMuons, "MaPM", "MaNV", chiTietPhieuMuon.MaPM);
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", chiTietPhieuMuon.MaSach);
            return View(chiTietPhieuMuon);
        }

        // GET: ChiTietPhieuMuon/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuMuon chiTietPhieuMuon = await db.ChiTietPhieuMuons.FindAsync(id);
            if (chiTietPhieuMuon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuMuon);
        }

        // POST: ChiTietPhieuMuon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChiTietPhieuMuon chiTietPhieuMuon = await db.ChiTietPhieuMuons.FindAsync(id);
            db.ChiTietPhieuMuons.Remove(chiTietPhieuMuon);
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
