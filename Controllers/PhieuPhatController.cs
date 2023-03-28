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
    public class PhieuPhatController : Controller
    {
        private thuvienEntities db = new thuvienEntities();

        // GET: PhieuPhat
        public async Task<ActionResult> Index()
        {
            var phieuPhats = db.PhieuPhats.Include(p => p.PhieuMuon);
            return View(await phieuPhats.ToListAsync());
        }

        // GET: PhieuPhat/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuPhat phieuPhat = await db.PhieuPhats.FindAsync(id);
            if (phieuPhat == null)
            {
                return HttpNotFound();
            }
            return View(phieuPhat);
        }

        // GET: PhieuPhat/Create
        public ActionResult Create()
        {
            ViewBag.MaPM = new SelectList(db.PhieuMuons, "MaPM", "MaNV");
            return View();
        }

        // POST: PhieuPhat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaPP,MaPM,LyDo")] PhieuPhat phieuPhat)
        {
            if (ModelState.IsValid)
            {
                db.PhieuPhats.Add(phieuPhat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaPM = new SelectList(db.PhieuMuons, "MaPM", "MaNV", phieuPhat.MaPM);
            return View(phieuPhat);
        }

        // GET: PhieuPhat/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuPhat phieuPhat = await db.PhieuPhats.FindAsync(id);
            if (phieuPhat == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPM = new SelectList(db.PhieuMuons, "MaPM", "MaNV", phieuPhat.MaPM);
            return View(phieuPhat);
        }

        // POST: PhieuPhat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaPP,MaPM,LyDo")] PhieuPhat phieuPhat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuPhat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaPM = new SelectList(db.PhieuMuons, "MaPM", "MaNV", phieuPhat.MaPM);
            return View(phieuPhat);
        }

        // GET: PhieuPhat/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuPhat phieuPhat = await db.PhieuPhats.FindAsync(id);
            if (phieuPhat == null)
            {
                return HttpNotFound();
            }
            return View(phieuPhat);
        }

        // POST: PhieuPhat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PhieuPhat phieuPhat = await db.PhieuPhats.FindAsync(id);
            db.PhieuPhats.Remove(phieuPhat);
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
