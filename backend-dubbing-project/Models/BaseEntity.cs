using System.ComponentModel.DataAnnotations;

namespace SoftServe.ITAcademy.BackendDubbingProject.Models
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}