﻿using Microsoft.AspNetCore.Http;
using SyncSpace.Domain.Exceptions;
using System.Security.Claims;

namespace SyncSpace.Application.ApplicationUser;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser GetCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user == null)
            throw new InvalidOperationException("User context is not present");

        if (user.Identity == null || !user.Identity.IsAuthenticated)
            throw new CustomeException("User is not Authenticated");

        var userId = user.FindFirst(c => c.Type == "uid")!.Value;
        var userEmail = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
        return new CurrentUser(userId, userEmail, roles);
    }
}
