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
    public class HandlersController : Controller
    {
        private DomainRegisterContext db = new DomainRegisterContext();

        // GET: Handlers
        public async Task<ActionResult> Index()
        {
            return View(await db.Handlers.ToListAsync());
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
        public async Task<ActionResult> Create([Bind(Include = "HandlerId,HandlerName,FirstName,LastName,Email")] Handler handler)
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
        public async Task<ActionResult> Edit([Bind(Include = "HandlerId,HandlerName,FirstName,LastName,Email")] Handler handler)
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
