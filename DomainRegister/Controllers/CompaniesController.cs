using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using DomainRegister.Models;

namespace DomainRegister.Controllers
{
    public class CompaniesController : Controller
    {
        private DomainRegisterContext db = new DomainRegisterContext();

        // GET: Companies
        public async Task<ActionResult> Index(string sort)
        {
            ViewBag.CompanySortParam = String.IsNullOrEmpty(sort) ? "company_desc" : "";
            ViewBag.HandlerSortParam = sort == "handler" ? "handler_desc" : "handler";

            //var companies = db.Companies.Include(c => c.Handler);
            var companies = (from c in db.Companies select c).Include(c => c.Handler);

            switch (sort)
            {
                case "company_desc":
                    companies = companies.OrderByDescending(c => c.CompanyName);
                    break;
                case "handler":
                    companies = companies.OrderBy(c => c.Handler.FirstName);
                    break;
                case "handler_desc":
                    companies = companies.OrderByDescending(c => c.Handler.FirstName);
                    break;
                default:
                    companies = companies.OrderBy(c => c.CompanyName);
                    break;
            }

            return View(await companies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.HandlerId = new SelectList(db.Handlers, "HandlerId", "FirstName");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompanyId,CompanyName,HandlerId")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HandlerId = new SelectList(db.Handlers, "HandlerId", "FirstName", company.HandlerId);
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.HandlerId = new SelectList(db.Handlers, "HandlerId", "FirstName", company.HandlerId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompanyId,CompanyName,HandlerId")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HandlerId = new SelectList(db.Handlers, "HandlerId", "FirstName", company.HandlerId);
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Company company = await db.Companies.FindAsync(id);
            db.Companies.Remove(company);
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
