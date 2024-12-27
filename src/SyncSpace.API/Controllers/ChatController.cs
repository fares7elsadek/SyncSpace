using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SyncSpace.API.SignalR.Hubs;
using SyncSpace.Application.ChatRoom.Commands;
using SyncSpace.Application.ChatRoom.Queries.GetRoomChat;
using SyncSpace.Domain.Helpers;
using System.Net;

namespace SyncSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ApiResponse apiResponse;
        private readonly IHubContext<StreamingHub> _hubContext;
        public ChatController(IMediator mediator, IHubContext<StreamingHub> hubContext)
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
        public async Task<ActionResult<ApiResponse>> SendMessage([FromBody] SendMessageCommand command)
        {
            var message = await _mediator.Send(command);
            await _hubContext.Clients.Group(message.RoomId).SendAsync("ReceiveMessage", message);
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = message;
            return Ok(apiResponse);
        }

        [HttpGet("{roomId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetRoomChat([FromRoute] string roomId)
        {
            var messages = await _mediator.Send(new GetRoomChatQuery(roomId));
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = messages;
            return Ok(apiResponse);
        }
    }
}
