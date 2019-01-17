using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SoftServe.ITAcademy.BackendDubbingProject.Models
{
    public class Performance : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Speech> Speeches { get; set; }
    }
}