using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Http;
using GraphQL.Instrumentation;
using GraphQL.Types;
using Market.Infrastructure;
using Market.Inventory;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Market.Controllers
{
    public class GraphQLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GraphQLSettings _settings;
        private readonly IDocumentWriter _writer;
        private readonly IDocumentExecuter _executor;

        public GraphQLMiddleware(RequestDelegate next, GraphQLSettings settings, IDocumentWriter writer, IDocumentExecuter executor)
        {
            _next = next;
            _settings = settings;
            _writer = writer;
            _executor = executor;
        }

        public async Task InvokeAsync(HttpContext httpContext, ISchema schema, IServiceProvider serviceProvider)
        {
            try
            {
                var start = DateTime.UtcNow;
                if (IsGraphQLRequest(httpContext))
                {
                    string body;
                    using (var streamReader = new StreamReader(httpContext.Request.Body))
                    {
                        body = await streamReader.ReadToEndAsync();

                        var request = JsonConvert.DeserializeObject<InventoryRequest>(body);

                        var result = await _executor.ExecuteAsync(_ =>
                        {
                            _.Schema = schema;
                            _.ExposeExceptions = true;
                            _.Query = request.Query;
                            _.OperationName = request.OperationName;
                            _.Inputs = request.Variables.ToInputs();
                            _.UserContext = _settings.BuildUserContext?.Invoke(httpContext);
                            _.Listeners.Add(serviceProvider.GetRequiredService<DataLoaderDocumentListener>());
                           
                        }).ConfigureAwait(false);                     

                        await WriteResult(httpContext, result);
                    }
                }
                else
                {
                    await _next(httpContext);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task WriteResult(HttpContext httpContext, ExecutionResult result)
        {
            var json = _writer.Write(result);
            httpContext.Response.ContentType = "application/json";          
            httpContext.Response.StatusCode = result.Errors?.Any() == true ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;
            await httpContext.Response.WriteAsync(json);
        }

        private bool IsGraphQLRequest(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments(_settings.Path)
                && string.Equals(context.Request.Method, "POST", StringComparison.OrdinalIgnoreCase);
        }
        private void CheckForErrors(ExecutionResult result)
        {
            if (result.Errors?.Count > 0)
            {
                var errors = new List<Exception>();
                foreach (var error in result.Errors)
                {
                    var ex = new Exception(error.Message);
                    if (error.InnerException != null)
                    {
                        ex = new Exception(error.Message, error.InnerException);
                    }
                    errors.Add(ex);
                }
                throw new AggregateException(errors);
            }
        }
        public static T Deserialize<T>(Stream s)
        {
            using (var reader = new StreamReader(s))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var ser = new JsonSerializer();
                return ser.Deserialize<T>(jsonReader);
            }
        }
    }
}
