using AutoMapper;
using MediatR;
using SyncSpace.Application.ApplicationUser.Dtos;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.ApplicationUser.Queries.GetUserDetails;

public class GetUserDetailsQueryHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetUserDetailsQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.User.GetOrDefalutAsync(u => u.Id == request.UserId);
        if(user == null)
            throw new NotFoundException(nameof(user),request.UserId);
        var userDto = mapper.Map<UserDto>(user);
        return userDto;
    }
}
