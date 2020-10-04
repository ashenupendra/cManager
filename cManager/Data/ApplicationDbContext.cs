using cManager.Shared;
using cManager.Shared.Accounting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace cManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountTransaction>()
                .HasOne(p => p.FromAccount)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.FromAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AccountTransaction>()
                .HasOne(p => p.ToAccount)
                .WithMany(b => b.Received)
                .HasForeignKey(p => p.ToAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var auditableEntity in ChangeTracker.Entries<Auditable>())
            {
                if (auditableEntity.State == EntityState.Added ||
                    auditableEntity.State == EntityState.Modified)
                {
                    auditableEntity.Entity.ModifiedTime = DateTime.Now;
                    auditableEntity.Entity.CreatedBy = "ToDo";

                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.CreatedTime = DateTime.Now;
                        auditableEntity.Entity.ModifiedBy = "ToDo";
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
