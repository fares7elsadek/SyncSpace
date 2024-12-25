using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SyncSpace.API.SignalR.Hubs;
using SyncSpace.Application.Stream.Commands.ChangeStream;
using SyncSpace.Application.Stream.Commands.PauseStream;
using SyncSpace.Application.Stream.Commands.PlayStream;
using SyncSpace.Application.Stream.Commands.StartStream;
using SyncSpace.Application.Stream.Commands.SyncStream;
using SyncSpace.Application.Stream.Queries.GetStreamDetails;
using SyncSpace.Domain.Helpers;
using System.Net;

namespace SyncSpace.API.Controllers
{
    [Route("api/rooms/{RoomId}/[controller]")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ApiResponse apiResponse;
        private readonly IHubContext<StreamingHub> _hubContext;
        public StreamController(IMediator mediator,
            IHubContext<StreamingHub> hubContext)
        {
            _mediator = mediator;
            apiResponse = new ApiResponse();
            _hubContext = hubContext;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> StartStream([FromRoute]string RoomId, [FromBody] StartStreamCommand command)
        {
            command.RoomId = RoomId;
            await _mediator.Send(command);
            await _hubContext.Clients.Group(RoomId).SendAsync("StreamStarted", command.VideoUrl);
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = "Stream started !";
            return Ok(apiResponse);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> ChangeStream([FromRoute] string RoomId, [FromBody] ChangeStreamCommand command)
        {
            command.RoomId = RoomId;
            await _mediator.Send(command);
            await _hubContext.Clients.Group(RoomId).SendAsync("StreamChanged", command.VideoUrl);
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = "Stream Changed !";
            return Ok(apiResponse);
        }

        [HttpPost("pause")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> PauseStream([FromRoute] string RoomId)
        {
            
            await _mediator.Send(new PauseStreamCommand(RoomId));
            await _hubContext.Clients.Group(RoomId).SendAsync("StreamPaused");
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = "Stream Paused !";
            return Ok(apiResponse);
        }


        [HttpPost("play")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> PlayStream([FromRoute] string RoomId)
        {

            await _mediator.Send(new PlayStreamCommand(RoomId));
            await _hubContext.Clients.Group(RoomId).SendAsync("StreamResumed");
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = "Stream resumed !";
            return Ok(apiResponse);
        }

        [HttpPost("sync")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> SyncStream([FromRoute] string RoomId, [FromBody] SyncStreamCommand command)
        {
            command.RoomId = RoomId;
            await _mediator.Send(command);
            await _hubContext.Clients.Group(RoomId).SendAsync("StreamSynced", command.Time);
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = "Stream synced !";
            return Ok(apiResponse);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetStreamDetails([FromRoute] string RoomId)
        {
            var command = new GetStreamDetailsQuery(RoomId);
            var Stream = await _mediator.Send(command);
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = Stream;
            return Ok(apiResponse);
        }

    }
}
