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

        public override bool Equals(object obj)
        {
            return (Id == (obj as Language).Id) && (Name == (obj as Language).Name);
        }
    }
}
