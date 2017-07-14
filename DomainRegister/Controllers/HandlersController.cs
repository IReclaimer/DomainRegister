using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using DomainRegister.Models;
using X.PagedList;

namespace DomainRegister.Controllers
{
    public class HandlersController : Controller
    {
        private DomainRegisterContext db = new DomainRegisterContext();

        // GET: Handlers
        public async Task<ActionResult> Index(string sort, int? page)
        {
            ViewBag.FirstSortParam = String.IsNullOrEmpty(sort) ? "first_desc" : "";
            ViewBag.LastSortParam = sort == "last" ? "last_desc" : "last";

            var handlers = from h in db.Handlers select h;

            switch (sort)
            {
                case "first_desc":
                    handlers = handlers.OrderByDescending(h => h.FirstName);
                    break;
                case "last":
                    handlers = handlers.OrderBy(h => h.LastName);
                    break;
                case "last_desc":
                    handlers = handlers.OrderByDescending(h => h.LastName);
                    break;
                default:
                    handlers = handlers.OrderBy(h => h.FirstName);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentPagedList = await handlers.ToPagedListAsync(pageNumber, pageSize);
            return View(ViewBag.CurrentPagedList);
        }

        // GET: Handlers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Handler handler = await db.Handlers.FindAsync(id);
            if (handler == null)
            {
                return HttpNotFound();
            }
            if(TempData["handlerHasChildrenDeletionWarning"] != null)
                ViewBag.DeletionWarning = TempData["handlerHasChildrenDeletionWarning"].ToString();

            return View(handler);
        }

        // GET: Handlers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Handlers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "HandlerId,FirstName,LastName,Email")] Handler handler)
        {
            if (ModelState.IsValid)
            {
                db.Handlers.Add(handler);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(handler);
        }

        // GET: Handlers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Handler handler = await db.Handlers.FindAsync(id);
            if (handler == null)
            {
                return HttpNotFound();
            }
            return View(handler);
        }

        // POST: Handlers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "HandlerId,FirstName,LastName,Email")] Handler handler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(handler).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(handler);
        }

        // GET: Handlers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Handler handler = await db.Handlers.FindAsync(id);
            if (handler == null)
            {
                return HttpNotFound();
            }
            if (handler.Companies.Any())
            {
                TempData["handlerHasChildrenDeletionWarning"] = "This handler has companies, " +
                    "please move these to another handler, or delete them seperately, before " +
                    "attempting to delete this hander.";
                return RedirectToAction("Details", new { id = id });
            }
            return View(handler);
        }

        // POST: Handlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Handler handler = await db.Handlers.FindAsync(id);
            db.Handlers.Remove(handler);
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
