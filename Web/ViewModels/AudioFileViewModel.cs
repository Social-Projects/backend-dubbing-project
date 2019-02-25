using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace Web.ViewModels
{
    public class AudioFileViewModel
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public int SpeechId { get; set; }

        public int LanguageId { get; set; }

        public IFormFile File { get; set; }

        [JsonIgnore]
        public byte[] AudioFile { get; set; }
    }
}
