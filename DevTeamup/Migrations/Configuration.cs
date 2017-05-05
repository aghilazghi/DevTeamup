using DevTeamup.Models;

namespace DevTeamup.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DevTeamup.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DevTeamup.Models.ApplicationDbContext context)
        {
            context.DevelopmentLanguages.AddOrUpdate(
                l => l.Id,
                new DevelopmentLanguage
                {
                    Id = 1,
                    Name = "C"
                },
                new DevelopmentLanguage
                {
                    Id = 2,
                    Name = "C++"
                },
                new DevelopmentLanguage
                {
                    Id = 3,
                    Name = "C#"
                },
                new DevelopmentLanguage
                {
                    Id = 4,
                    Name = "Java"
                },
                new DevelopmentLanguage
                {
                    Id = 5,
                    Name = "JavaScript"
                },
                new DevelopmentLanguage
                {
                    Id = 6,
                    Name = "Objective C"
                },
                new DevelopmentLanguage
                {
                    Id = 7,
                    Name = "PHP"
                },
                new DevelopmentLanguage
                {
                    Id = 8,
                    Name = "Python"
                },
                new DevelopmentLanguage
                {
                    Id = 9,
                    Name = "Ruby"
                },
                new DevelopmentLanguage
                {
                    Id = 10,
                    Name = "Swift"
                },
                new DevelopmentLanguage
                {
                    Id = 11,
                    Name = "Visual Basic .NET"
                }
            );

            context.DevelopmentTypes.AddOrUpdate(
                t => t.Id,
                new DevelopmentType
                {
                    Id = 1,
                    Name = "API Development"
                },
                new DevelopmentType
                {
                    Id = 2,
                    Name = "Back-end Development"
                },
                new DevelopmentType
                {
                    Id = 3,
                    Name = "Data Science"
                },
                new DevelopmentType
                {
                    Id = 4,
                    Name = "Desktop Development"
                },
                new DevelopmentType
                {
                    Id = 5,
                    Name = "Front-end Development"
                },
                new DevelopmentType
                {
                    Id = 6,
                    Name = "Game Development"
                },
                new DevelopmentType
                {
                    Id = 7,
                    Name = "Mobile Development"
                },
                new DevelopmentType
                {
                    Id = 8,
                    Name = "Web Development"
                }
            );
        }
    }
}
