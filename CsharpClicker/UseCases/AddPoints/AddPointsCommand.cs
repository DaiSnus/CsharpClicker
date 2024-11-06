using MediatR;

namespace CsharpClicker.UseCases.AddPoints;

public record AddPointsCommand(int Times, bool IsAuto = false) : IRequest<Unit>;