﻿using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MedicalSystemAPI.Filters
{
    public class SwaggerHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            if (context.MethodInfo.GetCustomAttribute(typeof(SwaggerHeaderAttribute)) is SwaggerHeaderAttribute attribute)
            {
                var existingParam = operation.Parameters.FirstOrDefault(p =>
                    p.In == ParameterLocation.Header && p.Name == attribute.HeaderName);
                if (existingParam != null) // remove description from [FromHeader] argument attribute
                {
                    operation.Parameters.Remove(existingParam);
                }

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = attribute.HeaderName,
                    In = ParameterLocation.Header,
                    Description = attribute.Description,
                    Required = attribute.IsRequired,
                    Schema = string.IsNullOrEmpty(attribute.DefaultValue)
                        ? null
                        : new OpenApiSchema
                        {
                            Type = "String",
                            Default = new OpenApiString(attribute.DefaultValue)
                        }
                });
            }
        }
    }
    public class SwaggerHeaderAttribute : Attribute
    {
        public string HeaderName { get; }
        public string Description { get; }
        public string DefaultValue { get; }
        public bool IsRequired { get; }

        public SwaggerHeaderAttribute(string headerName, string description = null, string defaultValue = null, bool isRequired = false)
        {
            HeaderName = headerName;
            Description = description;
            DefaultValue = defaultValue;
            IsRequired = isRequired;
        }
    }
}
