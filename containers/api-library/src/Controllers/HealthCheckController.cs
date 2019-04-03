using Framework.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route(RouteNames.HealthCheck)]
    public class HealthCheckController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
