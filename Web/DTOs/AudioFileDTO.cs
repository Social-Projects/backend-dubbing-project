using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.DTOs
{
    public class AudioFileDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string FileName { get; set; }

        [Required]
        public IFormFile File { get; set; }

        [Required]
        public int SpeechId { get; set; }

        [Required]
        public int LanguageId { get; set; }
    }
}