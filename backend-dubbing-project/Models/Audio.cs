using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SoftServe.ITAcademy.BackendDubbingProject.Models
{
    public class Audio : BaseEntity
    {
        [NotMapped]
        public IFormFile AudioFile { get; set; }

        public string FileName { get; set; }

        public int LanguageId { get; set; }

        public int SpeechId { get; set; }

        [JsonIgnore]
        public virtual Language Language { get; set; }

        [JsonIgnore]
        public virtual Speech Speech { get; set; }
    }
}
