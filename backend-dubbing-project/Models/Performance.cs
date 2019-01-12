using System.ComponentModel.DataAnnotations;

namespace Dubbing.Models
{
    public class Performance : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
