using MediatR;

namespace CsharpClicker.UseCases.Register;

public record RegisterCommand(string UserName, string Password) : IRequest<Unit>;