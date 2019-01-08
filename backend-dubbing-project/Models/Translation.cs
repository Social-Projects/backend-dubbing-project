using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace backend_dubbing_project.Models
{
    public class Translation
    {
        public int Id { get; set; }

        public IFormFile AudioFile { get; set; }

        public string Text { get; set; }
    }
}
