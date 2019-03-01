using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.DTOs
{
    public class AudioFileDTO
    {
        public int Id { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}