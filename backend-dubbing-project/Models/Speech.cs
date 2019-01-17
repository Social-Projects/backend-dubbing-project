using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace SoftServe.ITAcademy.BackendDubbingProject.Models
{
    public class Speech : BaseEntity
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public int PerformanceId { get; set; }

        [JsonIgnore]
        public virtual Performance Performance { get; set; }

        [JsonIgnore]
        public virtual ICollection<Audio> Audios { get; set; }

        [NotMapped]
        public int Duration
        {
            get
            {
                if (Audios != null)
                    return Audios.Max(x => x.Duration);
                else
                    return 0;
            }
        }
    }
}