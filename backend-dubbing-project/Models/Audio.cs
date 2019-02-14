using Newtonsoft.Json;

namespace SoftServe.ITAcademy.BackendDubbingProject.Models
{
    public class Audio : BaseEntity
    {
        public string FileName { get; set; }

        public int LanguageId { get; set; }

        public int SpeechId { get; set; }

        [JsonIgnore]
        public virtual Language Language { get; set; }

        [JsonIgnore]
        public virtual Speech Speech { get; set; }

        [JsonIgnore]
        public int Duration { get; set; }
    }
}
