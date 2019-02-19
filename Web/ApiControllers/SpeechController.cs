using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeechController : Controller
    {
        private readonly ISpeechService _speechService;

        public SpeechController(ISpeechService speechService)
        {
            _speechService = speechService;
        }
    }
}