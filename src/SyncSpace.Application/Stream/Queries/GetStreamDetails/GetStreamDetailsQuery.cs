using MediatR;
using SyncSpace.Application.Stream.Dtos;

namespace SyncSpace.Application.Stream.Queries.GetStreamDetails;

public class GetStreamDetailsQuery(string roomId):IRequest<StreamDetails>
{
    public string RoomId { get; set; } = roomId;
}
