namespace CsharpClicker.Infrastructure.Abstractions;

public interface ICurrentUserAccessor
{
    Guid GetCurrentUserId();
}
