using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using DomainRegister.Models;

namespace DomainRegister.Controllers
{
    public class AuditLogsController : Controller
    {
        private DomainRegisterContext db = new DomainRegisterContext();

        // GET: AuditLogs
        public async Task<ActionResult> Index()
        {
            return View(await db.AuditLogs.ToListAsync());
        }

        // GET: AuditLogs/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditLog auditLog = await db.AuditLogs.FindAsync(id);
            if (auditLog == null)
            {
                return HttpNotFound();
            }
            return View(auditLog);
        }

        // GET: AuditLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuditLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuditLogID,Action,TableName,ColumnPrimaryKey,ColumnName,OldValue,NewValue,ModifiedDateTime,UserID")] AuditLog auditLog)
        {
            if (ModelState.IsValid)
            {
                auditLog.AuditLogID = Guid.NewGuid();
                db.AuditLogs.Add(auditLog);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(auditLog);
        }

        // GET: AuditLogs/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditLog auditLog = await db.AuditLogs.FindAsync(id);
            if (auditLog == null)
            {
                return HttpNotFound();
            }
            return View(auditLog);
        }

        // POST: AuditLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuditLogID,Action,TableName,ColumnPrimaryKey,ColumnName,OldValue,NewValue,ModifiedDateTime,UserID")] AuditLog auditLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auditLog).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(auditLog);
        }

        // GET: AuditLogs/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditLog auditLog = await db.AuditLogs.FindAsync(id);
            if (auditLog == null)
            {
                return HttpNotFound();
            }
            return View(auditLog);
        }

        // POST: AuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            AuditLog auditLog = await db.AuditLogs.FindAsync(id);
            db.AuditLogs.Remove(auditLog);
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
