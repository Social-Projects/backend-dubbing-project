using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SoftServe.ITAcademy.BackendDubbingProject.Models
{
    public class Language : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Audio> Audios { get; set; }
    }
}
