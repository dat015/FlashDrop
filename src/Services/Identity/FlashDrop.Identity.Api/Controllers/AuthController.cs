using FlashDrop.Identity.Application.Features.Login;
using FlashDrop.Identity.Application.Features.Logout;
using FlashDrop.Identity.Application.Features.RefreshToken;
using FlashDrop.Identity.Application.Features.Register;
using FlashDrop.Shared.Attributes;
using FlashDrop.Shared.Controller;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace FlashDrop.Identity.Api.Controllers;

[Route("api/[controller]")]
public class AuthController : BaseApiController
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [RateLimit(5)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        return CreatedResponse(
            result,
            "Register successfully.");
    }

    [HttpPost("login")]
    [RateLimit(5)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);

        return Success(
            result,
            "Login successfully.");
    }
    [RateLimit(5)]
    [Authorize]
    [HttpDelete("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
    {
        await _mediator.Send(command);

        return Success(
            new {},
            "Logout successfully.");
    }

    [HttpPost("refresh")]
    [RateLimit(5)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);

        return Success(
            result,
            "Token refreshed successfully.");
    }
}