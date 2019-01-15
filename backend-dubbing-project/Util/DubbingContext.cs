using Microsoft.EntityFrameworkCore;
using Dubbing.Models;
namespace Dubbing.Util
{
    public class DubbingContext : DbContext
    {
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Audio> Audios { get; set; }
        public DbSet<Language> Languages { get; set;}
        public DbSet<Speech> Speeches { get; set; }
        public DubbingContext(DbContextOptions<DubbingContext> options) : base(options)
        { }

     
    }
}
