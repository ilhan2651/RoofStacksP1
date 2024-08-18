using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class TokenEndpointOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var path = context.ApiDescription.RelativePath;
        if (path.Contains("connect/token"))
        {
            operation.Summary = "Obtain JWT token";
            operation.Description = "Use this endpoint to obtain a JWT token.";
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/x-www-form-urlencoded"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["grant_type"] = new OpenApiSchema { Type = "string", Example = new OpenApiString("client_credentials") },
                                ["client_id"] = new OpenApiSchema { Type = "string" },
                                ["client_secret"] = new OpenApiSchema { Type = "string" },
                                ["scope"] = new OpenApiSchema { Type = "string" }
                            }
                        }
                    }
                }
            };
        }
    }
}
