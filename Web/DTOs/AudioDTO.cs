using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.DTOs
{
    public class AudioDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string FileName { get; set; }

        [Required]
        public int SpeechId { get; set; }

        [Required]
        public int LanguageId { get; set; }
    }
}