using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace SyncSpace.Application.Services;

public interface IAuthService
{
    Task<JwtSecurityToken> CreateJwtToken(User user);
    Task<AuthResponse> GetJwtToken(User user,List<string> roles);
    RefreshToken GenerateRefreshToken();
}
