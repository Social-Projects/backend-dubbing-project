using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class SpeechViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int Duration { get; set; }
    }
}
