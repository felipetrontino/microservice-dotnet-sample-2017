using Framework.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Controllers
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
