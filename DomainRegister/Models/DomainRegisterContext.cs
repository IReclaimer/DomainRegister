using DomainRegister.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace DomainRegister.Models
{
    public class DomainRegisterContext : DbContext
    {
        public DomainRegisterContext() : base()
        {
            Database.SetInitializer(new DomainDBInitializer());
        }

        public DbSet<Domain> Domains { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Handler> Handlers { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Handler>().Property(p => p.FirstName)
                .HasMaxLength(15)
                .IsRequired()
                .HasUniqueIndexAnnotation("UQ_Handler_FullName", 0);

            modelBuilder.Entity<Handler>().Property(p => p.LastName)
                .HasMaxLength(15)
                .IsRequired()
                .HasUniqueIndexAnnotation("UQ_Handler_FullName", 1);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            var entityChanges = this.ChangeTracker.Entries()
                .Where(t => (t.State == EntityState.Added) | (t.State ==EntityState.Modified) | (t.State == EntityState.Deleted))
                .Select(t => t.Entity)
                .ToArray();

            foreach (var entity in entityChanges)
            {
                foreach (var column in entity.OriginalValues.PropertyNames)
                {
                    var auditLog = new AuditLog() //fill the AuditLog entity of EF
                    {
                        AuditLogID = Guid.NewGuid(),
                        Action = entity.State,
                        TableName = entity.Entity.ToString(),
                        ColumnPrimaryKey = entity.,
                        ColumnName = column.ToString(),
                        OldValue = entity.OriginalValues.,
                        NewValue = entity.NewValue,
                        ModifiedDateTime = entity.ModifiedDateTime,
                        UserID = entity.UserID
                    };

                    this.AuditLogs.Add(auditLog); //store audit details into DB table
                }
            }

            return base.SaveChanges();
        }
    }
}