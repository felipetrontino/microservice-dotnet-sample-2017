using Framework.Core.Pagination;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Framework.Web.Common
{
    public class ApiController : ControllerBase
    {
        public virtual OkObjectResult PagedOk<T>(PagedResponse<T> result)
        {
            var metadata = new
            {
                result.TotalCount,
                result.PageSize,
                result.Page,
                result.TotalPages,
                result.HasNext,
                result.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(result);
        }
    }
}