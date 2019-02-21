using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class PerformanceViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
