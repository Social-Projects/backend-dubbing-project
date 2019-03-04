using System.ComponentModel.DataAnnotations;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.DTOs
{
    public class SpeechDTO
    {
        public int Id { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        [StringLength(128)]
        public string Text { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int PerformanceId { get; set; }
    }
}