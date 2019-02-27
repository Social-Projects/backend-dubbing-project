using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.DTOs
{
    public class PerformanceDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Title { get; set; }
    }
}