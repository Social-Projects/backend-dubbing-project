using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Dubbing.Models
{
    public class Audio : BaseEntity
    {
        [Required]
        [NotMapped]
        [JsonIgnore]
        public IFormFile AudioFile { get; set; }
        string _fileName;
        public string FileName
        {
            get => AudioFile.FileName;
            set =>_fileName = value;
        }

        [Required]
        public string Text { get; set; }

        public int LanguageId { get; set; }
        
        public int SpeechId { get; set; }
      
        [JsonIgnore]
        public virtual Language Language { get; set; }
        [JsonIgnore]
        public virtual Speech Speech { get; set; }
    }
}
