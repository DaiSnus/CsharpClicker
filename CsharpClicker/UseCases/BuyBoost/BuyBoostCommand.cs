using CsharpClicker.UseCases.Common;
using MediatR;

namespace CsharpClicker.UseCases.BuyBoost;

public record BuyBoostCommand(int BoostId) : IRequest<ScoreBoostDto>;