using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.ApplicationCore.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Infrastructure.Database
{
    public partial class DubbingContext : DbContext
    {
        public DbSet<Performance> Performances { get; set; }

        public DbSet<Audio> Audio { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Speech> Speeches { get; set; }

        public DubbingContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=dubbing.db");
        }
    }
}