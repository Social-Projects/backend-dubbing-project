using System.ComponentModel.DataAnnotations;

namespace SoftServe.ITAcademy.BackendDubbingProject.Models
{
    public class Language : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
