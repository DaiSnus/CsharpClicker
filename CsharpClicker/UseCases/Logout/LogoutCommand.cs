using MediatR;

namespace CsharpClicker.UseCases.Logout;

public record LogoutCommand : IRequest<Unit>;