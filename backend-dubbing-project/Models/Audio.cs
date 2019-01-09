using Microsoft.AspNetCore.Http;


namespace Dubbing.Models
{
    public class Audio
    {
        public int Id { get; set; }

        public IFormFile AudioFile { get; set; }

        public string Text { get; set; }

        //public string Language { get; set; }
    }
}
