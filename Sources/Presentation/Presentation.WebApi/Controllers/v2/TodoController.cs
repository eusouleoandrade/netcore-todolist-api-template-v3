using System.Diagnostics.CodeAnalysis;
using Core.Application.Dtos.Queries;
using Core.Application.Dtos.Wrappers;
using Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Controllers.Common;

namespace Presentation.WebApi.Controllers.v2
{
    [ExcludeFromCodeCoverage]
    [ApiVersion("2.0")]
    public class TodoController : BaseApiController
    {
        private readonly ICorrelationIdService _correlationIdService;

        private readonly ILogger<TodoController> _logger;

        public TodoController([FromServices] ICorrelationIdService correlationIdService, ILogger<TodoController> logger)
        {
            _correlationIdService = correlationIdService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<TodoQuery>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> Get()
        {
            // Test versioning
            IReadOnlyList<TodoQuery> todoQuery = new List<TodoQuery>
            {
                new TodoQuery(1, "Ir ao mercado", false),
                new TodoQuery(2, "Ir ao médico", false),
                new TodoQuery(3, "Fazer invesitimentos", false)
            };

            // Test correlationId context service
            _logger.LogInformation(_correlationIdService.GetCorrelationId());

            return Ok(await Task.FromResult(new Response<IReadOnlyList<TodoQuery>>(todoQuery, true)));
        }
    }
}