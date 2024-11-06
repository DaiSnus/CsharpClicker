using MediatR;

namespace CsharpClicker.UseCases.GetBoosts;

public record GetBoostsQuery : IRequest<IReadOnlyCollection<BoostDto>>;