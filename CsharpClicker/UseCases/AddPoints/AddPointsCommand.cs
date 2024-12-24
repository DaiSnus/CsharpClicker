using CsharpClicker.UseCases.Common;
using MediatR;

namespace CsharpClicker.UseCases.AddPoints;

public record AddPointsCommand(int Clicks, int Seconds) : IRequest<ScoreDto>;