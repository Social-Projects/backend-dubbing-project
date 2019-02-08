using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SoftServe.ITAcademy.BackendDubbingProject.Models
{
    public class AudioDTO
    {
        public IFormFile AudioFile { get; set; }
    }
}
