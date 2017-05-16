using DevTeamup.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;

namespace DevTeamup.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Teamup> Teamups { get; set; }
        public DbSet<DevelopmentType> DevelopmentTypes { get; set; }
        public DbSet<DevelopmentLanguage> DevelopmentLanguages { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Collaboration> Collaborations { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            modelBuilder.Entity<Collaboration>()
                .HasRequired(a => a.Teamup)
                .WithMany(t => t.Collaborations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Discussion>()
                .HasRequired(d => d.Teamup)
                .WithMany(t => t.Discussions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reply>()
                .HasRequired(r => r.Discussion)
                .WithMany(d => d.Replies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Favorite>()
                .HasRequired(a => a.Teamup)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserNotification>()
                .HasRequired(u => u.User)
                .WithMany(a => a.UserNotifications)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in ChangeTracker.Entries()
                .Where(
                    e => e.Entity is ITimeStamp &&
                         (e.State == EntityState.Added) ||
                         (e.State == EntityState.Modified)))
            {
                var e = (ITimeStamp)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    e.CreatedOn = DateTime.Now;
                }

                e.ModifiedOn = DateTime.Now;
            }
        }

        public override int SaveChanges()
        {
            ApplyRules();

            return base.SaveChanges();
        }
    }
}