using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities
{
    public class Speech : BaseEntity
    {
        public string Text { get; set; }

        [NotMapped]
        public int Duration { get; set; }

        public int PerformanceId { get; set; }

        public Performance Performance { get; set; }

        public ICollection<Audio> Audio { get; set; }
    }
}