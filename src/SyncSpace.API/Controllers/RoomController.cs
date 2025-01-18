using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SyncSpace.API.SignalR.Hubs;
using SyncSpace.Application.Room.Commands.AddUserToRoom;
using SyncSpace.Application.Room.Commands.CreateRoom;
using SyncSpace.Application.Room.Commands.JoinRoom;
using SyncSpace.Application.Room.Commands.LeaveRoom;
using SyncSpace.Application.Room.Commands.RemoveRoom;
using SyncSpace.Application.Room.Commands.RemoveUserFromRoom;
using SyncSpace.Application.Room.Commands.UpdateRoom;
using SyncSpace.Application.Room.Queries.GetAllRooms;
using SyncSpace.Application.Room.Queries.GetRoomById;
using SyncSpace.Application.Room.Queries.GetUserRooms;
using SyncSpace.Domain.Helpers;
using System.Net;

namespace SyncSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ApiResponse apiResponse;
        private readonly IHubContext<StreamingHub> _hubContext;
        public RoomController(IMediator mediator, IHubContext<StreamingHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
            apiResponse = new ApiResponse();
        }

        [HttpPost("new")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateRoom([FromBody] CreateRoomCommand command)
        {
            var room = await _mediator.Send(command);
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = room;
            return Ok(apiResponse);
        }

        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> UpdateRoom([FromBody] UpdateRoomCommand command)
        {
            var room = await _mediator.Send(command);
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = room;
            return Ok(apiResponse);
        }


        [HttpDelete("{roomId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> RemoveRoom([FromRoute] string roomId)
        {
            var command = new RemoveRoomCommand(roomId);
            await _mediator.Send(command);
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = "Room deleted successfully";
            return Ok(apiResponse);
        }

        [HttpPost("{RoomId}/{UserId}/add")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> AddUserToRoom([FromRoute] string RoomId, [FromRoute]
        string UserId)
        {
            var command = new AddUserToRoomCommand(RoomId, UserId);
            await _mediator.Send(command);
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = "User added successfully";
            return Ok(apiResponse);
        }

        [HttpPost("{RoomId}/join")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> JoinRoom([FromRoute] string RoomId, JoinRoomCommand command)
        {
            command.RoomId = RoomId;
            await _mediator.Send(command);
            await _hubContext.Groups.AddToGroupAsync(command.ConnectionId, RoomId);
            await _hubContext.Clients.Group(RoomId).SendAsync("UserJoined", command.ConnectionId);
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = "User Joined successfully";
            return Ok(apiResponse);
        }

        [HttpPost("{RoomId}/leave")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> LeaveRoom([FromRoute] string RoomId, LeaveRoomCommand command)
        {
            command.RoomId = RoomId;
            await _mediator.Send(command);
            await _hubContext.Groups.RemoveFromGroupAsync(command.ConnectionId, RoomId);
            await _hubContext.Clients.Group(RoomId).SendAsync("UserLeft", command.ConnectionId);
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = "User Leaved successfully";
            return Ok(apiResponse);
        }

        [HttpDelete("{RoomId}/{UserId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> RemoveUserFromRoom([FromRoute] string RoomId, [FromRoute]
        string UserId)
        {
            var command = new RemoveUserFromRoomCommand(RoomId, UserId);
            await _mediator.Send(command);
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = "User removed successfully";
            return Ok(apiResponse);
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetAllRooms()
        {
           
            var rooms = await _mediator.Send(new GetAllRoomsQuery());
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = rooms;
            return Ok(apiResponse);
        }

        [HttpGet("{RoomId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetRoomById(string RoomId)
        {

            var room = await _mediator.Send(new GetRoomByIdQuery(RoomId));
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = room;
            return Ok(apiResponse);
        }

        [HttpGet("user")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetUserRooms()
        {

            var rooms = await _mediator.Send(new GetUserRoomsQuery());
            apiResponse.IsSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = rooms;
            return Ok(apiResponse);
        }
    }
}
