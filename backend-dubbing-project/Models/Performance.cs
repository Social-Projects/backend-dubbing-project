using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dubbing.Models
{
    public class Performance : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Speech> Speeches{ get; set; }

    }
}
