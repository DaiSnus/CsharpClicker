using CsharpClicker.UseCases.BuyBoost;
using CsharpClicker.UseCases.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsharpClicker.Controllers;

[Route("boost")]
[Authorize]
public class BoostController : ControllerBase
{
    private readonly IMediator mediator;

    public BoostController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("buy")]
    public async Task<ScoreBoostDto> Buy(BuyBoostCommand command)
    {
        return await mediator.Send(command);
    }
}
