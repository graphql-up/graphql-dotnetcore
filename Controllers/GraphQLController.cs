using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Market.Inventory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Market.Controllers
{
    [Route("api/graphql")]
    public class GraphQLController : Controller
    {
       
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        private readonly IDocumentWriter _writer;

        public GraphQLController( IDocumentExecuter documentExecuter, ISchema schema, IDocumentWriter writer)
        {           
            _documentExecuter = documentExecuter;
            _schema = schema;
            _writer = writer;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]InventoryRequest request)
        {
            if (request == null) { throw new ArgumentNullException(nameof(request)); }

            var inputs = request.Variables.ToInputs();
            var queryToExecute = request.Query;

            var executionOptions = new ExecutionOptions { Schema = _schema, Query = queryToExecute, Inputs = inputs, OperationName = request.OperationName };

            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
