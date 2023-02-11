using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Core.Application.Dtos.Queries;
using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Wrappers;
using Core.Application.Interfaces.UseCases;
using Lib.Notification.Contexts;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Controllers.Common;

namespace Presentation.WebApi.Controllers.v1
{
    [ExcludeFromCodeCoverage]
    [ApiVersion("1.0")]
    public class TodoController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<TodoController> _logger;

        public TodoController(IMapper mapper, NotificationContext notificationContext, ILogger<TodoController> logger)
        {
            _mapper = mapper;
            _notificationContext = notificationContext;
            _logger = logger;
        }

        /// <summary>
        /// Get todos
        /// </summary>
        /// <param name="getAllTodoUseCase"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<TodoQuery>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> Get([FromServices] IGetAllTodoUseCase getAllTodoUseCase)
        {
            _logger.LogInformation(message: "Start controller {0} > method GetAll.", nameof(TodoController));

            var useCaseResponse = await getAllTodoUseCase.RunAsync();

            _logger.LogInformation("Finishes successfully controller {0} > method GetAll.", nameof(TodoController));

            return Ok(new Response<IReadOnlyList<TodoQuery>>(useCaseResponse, true));
        }

        /// <summary>
        /// Create todo
        /// </summary>
        /// <param name="request"></param>
        /// <param name="createTodoUseCase"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<CreateTodoQuery>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> Post([FromBody] CreateTodoRequest request, [FromServices] ICreateTodoUseCase createTodoUseCase)
        {
            _logger.LogInformation(message: "Start controller {0} > method {1}.", nameof(TodoController), nameof(Post));

            var useCaseResponse = await createTodoUseCase.RunAsync(_mapper.Map<CreateTodoUseCaseRequest>(request));

            if (createTodoUseCase.HasErrorNotification)
            {
                _notificationContext.AddErrorNotifications(createTodoUseCase);
                return BadRequest();
            }

            var response = _mapper.Map<CreateTodoQuery>(useCaseResponse);

            _logger.LogInformation("Finishes successfully controller {0} > method {1}.", nameof(TodoController), nameof(Post));

            return Created($"/api/v1/todo/{response.Id}", new Response<CreateTodoQuery>(data: response, succeeded: true));
        }

        /// <summary>
        /// Delete todo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleteTodoUsecase"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] int id, [FromServices] IDeleteTodoUseCase deleteTodoUsecase)
        {
            _logger.LogInformation(message: "Start controller {0} > method {1}.", nameof(TodoController), nameof(Delete));

            await deleteTodoUsecase.RunAsync(id);

            if (deleteTodoUsecase.HasErrorNotification)
            {
                _notificationContext.AddErrorNotifications(deleteTodoUsecase);
                return BadRequest();
            }

            _logger.LogInformation("Finishes successfully controller {0} > method {1}.", nameof(TodoController), nameof(Delete));

            return NoContent();
        }

        /// <summary>
        /// Get todo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="getTodoUseCase"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<GetTodoQuery>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> Get([FromRoute(Name = "id")] int id, [FromServices] IGetTodoUseCase getTodoUseCase)
        {
            _logger.LogInformation(message: "Start controller {0} > method {1}.", nameof(TodoController), nameof(Get));

            var useCaseResponse = await getTodoUseCase.RunAsync(id);

            if (getTodoUseCase.HasErrorNotification)
            {
                _notificationContext.AddErrorNotifications(getTodoUseCase);
                return BadRequest();
            }

            var response = _mapper.Map<GetTodoQuery>(useCaseResponse);

            _logger.LogInformation("Finishes successfully controller {0} > method {1}.", nameof(TodoController), nameof(Get));

            return Ok(new Response<GetTodoQuery>(succeeded: true, data: response));
        }

        /// <summary>
        /// Update todo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="updateTodoUseCase"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> Put([FromRoute(Name = "id")] int id, [FromBody] UpdateTodoRequest request, [FromServices] IUpdateTodoUseCase updateTodoUseCase)
        {
            _logger.LogInformation(message: "Start controller {0} > method {1}.", nameof(TodoController), nameof(Get));

            await updateTodoUseCase.RunAsync(new UpdateTodoUseCaseRequest(id, request.Title, request.Done));

            if (updateTodoUseCase.HasErrorNotification)
            {
                _notificationContext.AddErrorNotifications(updateTodoUseCase);
                return BadRequest();
            }

            _logger.LogInformation("Finishes successfully controller {0} > method {1}.", nameof(TodoController), nameof(Put));

            return Ok(new Response(succeeded: true));
        }

        /// <summary>
        /// Set done todo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="setDoneTodoUseCase"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> Patch([FromRoute(Name = "id")] int id, [FromBody] SetDoneTodoRequest request, [FromServices] ISetDoneTodoUseCase setDoneTodoUseCase)
        {
            _logger.LogInformation(message: "Start controller {0} > method {1}.", nameof(TodoController), nameof(Patch));

            await setDoneTodoUseCase.RunAsync(new SetDoneTodoUseCaseRequest(id, request.Done));

            if (setDoneTodoUseCase.HasErrorNotification)
            {
                _notificationContext.AddErrorNotifications(setDoneTodoUseCase);
                return BadRequest();
            }

            _logger.LogInformation("Finishes successfully controller {0} > method {1}.", nameof(TodoController), nameof(Patch));

            return Ok(new Response(succeeded: true));
        }
    }
}