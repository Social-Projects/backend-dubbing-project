using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Utilities
{
    public class DubbingContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Performance> Performances { get; set; }

        public DbSet<Audio> Audios { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Speech> Speeches { get; set; }

        public DubbingContext(DbContextOptions<DubbingContext> options)
        : base(options)
        {
        }

        public DubbingContext()
        {
        }
    }
}
