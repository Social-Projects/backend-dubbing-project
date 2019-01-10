using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace Dubbing.Models
{
    public class Audio
    {
        public int Id { get; set; }

        [Required]
        public IFormFile AudioFile { get; set; }

        [Required]
        public string Text { get; set; }

        //public string Language { get; set; }
    }
}
