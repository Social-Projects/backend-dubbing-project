using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.Database
{
    public partial class DatabaseContext : DbContext
    {
        public DbSet<Performance> Performances { get; set; }

        public DbSet<Audio> Audio { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Speech> Speeches { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
    }
}