using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using DomainRegister.Models;
using X.PagedList;

namespace DomainRegister.Controllers
{
    public class DomainsController : Controller
    {
        private DomainRegisterContext db = new DomainRegisterContext();

        // GET: Domains
        public async Task<ActionResult> Index(string sort, string currentSearch, string search, int? page)
        {
            ViewBag.CurrentSort = sort;
            ViewBag.DateSortParam = String.IsNullOrEmpty(sort) ? "date_desc" : "";
            ViewBag.CompanySortParam = sort == "company" ? "company_desc" : "company";
            ViewBag.HandlerSortParam = sort == "handler" ? "handler_desc" : "handler";

            if (search != null)
                page = 1;
            else
                search = currentSearch;

            ViewBag.CurrentSearch = search;

            var domains = db.Domains.Include(d => d.Company).Include(c => c.Company.Handler);

            if (!String.IsNullOrEmpty(search))
            {
                domains = domains.Where(d => d.DomainName.Contains(search) ||
                d.Company.CompanyName.Contains(search));
            }

            switch (sort)
            {
                case "date_desc":
                    domains = domains.OrderByDescending(d => d.RenewalDate);
                    break;
                case "company":
                    domains = domains.OrderBy(d => d.Company.CompanyName);
                    break;
                case "company_desc":
                    domains = domains.OrderByDescending(d => d.Company.CompanyName);
                    break;
                case "handler":
                    domains = domains.OrderBy(d => d.Company.Handler.FirstName);
                    break;
                case "handler_desc":
                    domains = domains.OrderByDescending(d => d.Company.Handler.FirstName);
                    break;
                default:
                    domains = domains.OrderBy(d => d.RenewalDate);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentPagedList = await domains.ToPagedListAsync(pageNumber, pageSize);
            return View(ViewBag.CurrentPagedList);
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
