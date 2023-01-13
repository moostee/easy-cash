using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EasyCash.Application.UseCases.Auth.Login;
using EasyCash.Application.UseCases.Users.ActivateorDeactivateUser;
using EasyCash.Application.UseCases.Users.AddUser;
using EasyCash.Application.UseCases.Users.GetAllUsers;
using EasyCash.Domain.Entity;

namespace EasyCash.Api.Controllers;

[Authorize(Roles = "ADMIN")]
public class UserController : BaseController
{
    public UserController(IMediator mediator) : base(mediator)
    {

    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginForm form)
    {
        form.Validate();
        return Ok(await _mediator.Send(new LoginCommand(form.Email, form.Password)));
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAsync(UserForm form)
    {
        form.Validate();
        return Ok(await _mediator.Send(new AddUserCommand(form.Name, form.Email, form.Password, form.Role)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserAsync()
    {
        return Ok(await _mediator.Send(new GetAllUsersQuery()));
    }

    [HttpPut("activate-deactivate")]
    public async Task<IActionResult> ActivateOrDeactivateUser(ActivateOrDeActivateUserForm form)
    {
        form.Validate();
        return Ok(await _mediator.Send(new ActivateorDeactivateUserCommand(form.UserId, form.Action)));
    }
}
