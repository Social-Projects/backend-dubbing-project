using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities
{
    internal class Speech : BaseEntity
    {
        public int Order { get; set; }

        public string Text { get; set; }

        [NotMapped]
        public int Duration { get; set; }

        public int PerformanceId { get; set; }

        public Performance Performance { get; set; }

        public ICollection<Audio> Audios { get; set; }
    }
}