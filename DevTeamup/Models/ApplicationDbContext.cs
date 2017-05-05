using DevTeamup.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DevTeamup.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Teamup> Teamups { get; set; }
        public DbSet<DevelopmentType> DevelopmentTypes { get; set; }
        public DbSet<DevelopmentLanguage> DevelopmentLanguages { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }
    }
}