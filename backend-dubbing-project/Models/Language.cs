using System.ComponentModel.DataAnnotations;

namespace Dubbing.Models
{
    public class Language : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        
    }
}
