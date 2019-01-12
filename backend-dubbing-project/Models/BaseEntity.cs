using System.ComponentModel.DataAnnotations;

namespace Dubbing.Models
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}