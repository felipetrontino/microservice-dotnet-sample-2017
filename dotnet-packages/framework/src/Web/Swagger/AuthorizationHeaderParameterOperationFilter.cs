using Microsoft.AspNetCore.Mvc.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Web.Swagger
{
    public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var isAuthorized = filterPipeline.Any(x => x.Filter is AuthorizeFilter);
            var allowAnonymous = filterPipeline.Any(x => x.Filter is IAllowAnonymousFilter);

            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "Tenant",
                In = "header",
                Description = "Tenant",
                Required = true,
                Type = "string"
            });

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "UserName",
                In = "header",
                Description = "Username",
                Required = false,
                Type = "string"
            });

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "RequestId",
                In = "header",
                Description = "Request Id",
                Required = false,
                Type = "string"
            });

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "Culture",
                In = "header",
                Description = "Culture. pt-br, en-us, etc.",
                Required = false,
                Type = "string"
            });

            if (isAuthorized && !allowAnonymous)
            {
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "Authorization",
                    Default = "Bearer ",
                    In = "header",
                    Description = "Access Token",
                    Required = true,
                    Type = "string"
                });
            }
        }
    }
}