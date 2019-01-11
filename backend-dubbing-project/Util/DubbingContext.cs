using Microsoft.EntityFrameworkCore;
using Dubbing.Models;
namespace Dubbing.Util
{
    public class DubbingContext : DbContext
    {
        public DbSet<Performance> Performances { get; set; }
        public DubbingContext()
        {
        }
       
    }
}
