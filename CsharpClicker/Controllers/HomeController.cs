﻿using CsharpClicker.UseCases.AddPoints;
using CsharpClicker.UseCases.GetBoosts;
using CsharpClicker.UseCases.GetCurrentUser;
using CsharpClicker.ViewModels;
using MediatR;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;

namespace CsharpClicker.Controllers;

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

    [HttpPost("click")]
    public async Task<IActionResult> Click()
    {
        await mediator.Send(new AddPointsCommand(Times: 1));

        return RedirectToAction(nameof(Index));
    }
}
