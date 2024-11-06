using MediatR;

namespace CsharpClicker.UseCases.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<UserDto>;