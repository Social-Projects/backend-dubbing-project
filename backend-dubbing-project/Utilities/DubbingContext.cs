using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Utilities
{
    public class DubbingContext : DbContext
    {
        public DbSet<Performance> Performances { get; set; }

        public DubbingContext()
        {
        }
    }
}
