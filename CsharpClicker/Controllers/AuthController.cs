    using CsharpClicker.UseCases.Login;
using CsharpClicker.UseCases.Logout;
using CsharpClicker.UseCases.Register;
using CsharpClicker.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CsharpClicker.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    public readonly IMediator mediator;

    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand registerCommand)
    {
        try
        {
            await mediator.Send(registerCommand);
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            var viewModel = new AuthViewModel
            {
                UserName = registerCommand.UserName,
                Password = registerCommand.Password,
            };

            return View(viewModel);
        }

        return RedirectToAction(nameof(Login));
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View(new AuthViewModel());
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand loginCommand)
    {
        try
        {
            await mediator.Send(loginCommand);
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            var viewModel = new AuthViewModel
            {
                UserName = loginCommand.UserName,
                Password = loginCommand.Password,
            };

            return View(viewModel);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View(new AuthViewModel());
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutCommand command)
    {
        await mediator.Send(command);

        return RedirectToAction("login");
    }
}
