using MediatR;

namespace CsharpClicker.UseCases.Login;

public record LoginCommand(string UserName, string Password) : IRequest<Unit>;
