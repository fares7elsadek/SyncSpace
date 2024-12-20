namespace SyncSpace.Application.ApplicationUser;

public interface IUserContext
{
    public CurrentUser GetCurrentUser();
}
