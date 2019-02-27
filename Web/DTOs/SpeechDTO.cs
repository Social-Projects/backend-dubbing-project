using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.Database;

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
        public int PerformanceId { get; set; }
    }
}