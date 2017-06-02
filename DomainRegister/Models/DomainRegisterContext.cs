using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}