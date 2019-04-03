using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Framework.Web.Common
{
    public class FileOperation : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.OperationId.ToLower() == "apinotificacaocontatoimportarcontatocomgrupoasyncpost")
            {
                var remove = operation.Parameters.FirstOrDefault(w => w.Name == "file");
                operation.Parameters.Remove(remove);
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "File",
                    In = "formData",
                    Description = "Upload CSV",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("application/form-data");
            }
        }
    }
}
