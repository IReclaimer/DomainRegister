using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DomainRegister.Models;

namespace DomainRegister.Controllers
{
    public class DomainsController : Controller
    {
        private DomainRegisterContext db = new DomainRegisterContext();

        // GET: Domains
        public async Task<ActionResult> Index()
        {
            var domains = db.Domains.Include(d => d.Company);
            return View(await domains.ToListAsync());
        }

        // GET: Domains/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domain domain = await db.Domains.FindAsync(id);
            if (domain == null)
            {
                return HttpNotFound();
            }
            return View(domain);
        }

        // GET: Domains/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName");
            return View();
        }

        // POST: Domains/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DomainId,DomainName,RenewalDate,CompanyId")] Domain domain)
        {
            if (ModelState.IsValid)
            {
                db.Domains.Add(domain);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", domain.CompanyId);
            return View(domain);
        }

        // GET: Domains/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domain domain = await db.Domains.FindAsync(id);
            if (domain == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", domain.CompanyId);
            return View(domain);
        }

        // POST: Domains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DomainId,DomainName,RenewalDate,CompanyId")] Domain domain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(domain).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", domain.CompanyId);
            return View(domain);
        }

        // GET: Domains/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domain domain = await db.Domains.FindAsync(id);
            if (domain == null)
            {
                return HttpNotFound();
            }
            return View(domain);
        }

        // POST: Domains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Domain domain = await db.Domains.FindAsync(id);
            db.Domains.Remove(domain);
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
