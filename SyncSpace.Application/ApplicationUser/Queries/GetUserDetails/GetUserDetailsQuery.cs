using MediatR;
using SyncSpace.Application.ApplicationUser.Dtos;

namespace SyncSpace.Application.ApplicationUser.Queries.GetUserDetails;

public class GetUserDetailsQuery(string id):IRequest<UserDto>
{
    public string UserId { get; set; } = id;
}
