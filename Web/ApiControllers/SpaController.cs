using Microsoft.AspNetCore.Mvc;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
{
    public class SpaController : Controller
    {
        // GET
        public IActionResult Spa()
        {
            return File("~/spa/index.html", "text/html");
        }
    }
}