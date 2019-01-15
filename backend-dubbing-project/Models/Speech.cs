using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Dubbing.Models 
{
    public class Speech : BaseEntity
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public int PerformanceId { get; set;}

        [JsonIgnore]
        public virtual Performance Performance { get; set; }

        [JsonIgnore]
        public virtual ICollection<Audio> Audios { get; set;}
        
    }
}