using Microsoft.AspNetCore.Mvc;

namespace backend_dubbing_project.Controllers
{
    [Route("api")]
    [ApiController]
    
    public class Controllers : Controller
    {
        [HttpGet("HelloWorld")]
        public string Get()
        {
            return "Hello World!";
        } 
    }
}