using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainRegisterMailer.Models
{
    public class DomainRegisterContext : DbContext
    {
        public DomainRegisterContext() : base()
        {
            Database.SetInitializer(new DomainDBInitializer());
        }

        public DbSet<Domain> Domains { get; set; }
        public DbSet<Handler> Handlers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Handler>().Property(p => p.FirstName)
                .HasMaxLength(30)
                .IsRequired()
                .HasUniqueIndexAnnotation("UQ_Handler_FullName", 0);

            modelBuilder.Entity<Handler>().Property(p => p.LastName)
                .HasMaxLength(30)
                .IsRequired()
                .HasUniqueIndexAnnotation("UQ_Handler_FullName", 1);

            modelBuilder.Entity<Handler>().Property(p => p.Email)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnAnnotation("DataType", DataType.EmailAddress);

            base.OnModelCreating(modelBuilder);
        }
    }
}