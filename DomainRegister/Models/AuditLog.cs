using System;
using System.Data.Entity;

namespace DomainRegister.Models
{
    public class AuditLog
    {
        // Primary Key
        public Guid AuditLogID { get; set; }

        public EntityState Action { get; set; }
        public string TableName { get; set; }
        public string ColumnPrimaryKey { get; set; }
        public string ColumnName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public Guid? UserID { get; set; } //Temporarily nullable for testing
    }
}