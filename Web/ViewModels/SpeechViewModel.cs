using System.ComponentModel.DataAnnotations;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace Web.ViewModels
{
    public class SpeechViewModel
    {
        private IPerformanceService _performanceService;

        public SpeechViewModel(IPerformanceService performanceService)
        {
            _performanceService = performanceService;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int PerformanceId { get; set; }
    }
}