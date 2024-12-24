using CsharpClicker.UseCases.AddPoints;
using CsharpClicker.UseCases.Common;
using CsharpClicker.UseCases.GetBoosts;
using CsharpClicker.UseCases.GetCurrentUser;
using CsharpClicker.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;

namespace CsharpClicker.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IMediator mediator;

    public HomeController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var boosts = await mediator.Send(new GetBoostsQuery());
        var user = await mediator.Send(new GetCurrentUserQuery());

        var viewModel = new IndexViewModel()
        {
            Boosts = boosts,
            User = user
        };

        return View(viewModel);
    }

    [HttpPost("score")]
    public async Task<ScoreDto> Click(AddPointsCommand command)
        => await mediator.Send(command);
}
