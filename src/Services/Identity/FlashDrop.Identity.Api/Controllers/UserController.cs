using FlashDrop.Identity.Application.Features.Users.ChangePassword;
using FlashDrop.Identity.Application.Features.Users.CreateUser;
using FlashDrop.Identity.Application.Features.Users.DeleteUser;
using FlashDrop.Identity.Application.Features.Users.GetUser;
using FlashDrop.Identity.Application.Features.Users.GetUsers;
using FlashDrop.Identity.Application.Features.Users.UpdateUser;
using FlashDrop.Shared.Controller;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashDrop.Identity.Api.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // Restrict user management to Admin
public class UserController : BaseApiController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _mediator.Send(new GetUsersQuery());
        return Success(result, "Users retrieved successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var result = await _mediator.Send(new GetUserQuery(id));
        return Success(result, "User retrieved successfully.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedResponse(result, "User created successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new { success = false, message = "ID mismatch." });
        }

        var result = await _mediator.Send(command);
        return Success(result, "User updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id));
        return Success(result, "User deleted successfully.");
    }

    [HttpPut("{id}/change-password")]
    public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new { success = false, message = "ID mismatch." });
        }

        var result = await _mediator.Send(command);
        return Success(result, "Password changed successfully.");
    }
}
