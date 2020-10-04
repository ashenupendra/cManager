using cManager.Shared;
using cManager.Shared.Accounting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
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
                    auditableEntity.Entity.CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;

                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.CreatedTime = DateTime.Now;
                        auditableEntity.Entity.ModifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
