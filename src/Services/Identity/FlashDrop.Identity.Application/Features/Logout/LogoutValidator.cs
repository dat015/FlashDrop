using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Features.Logout
{
    public class LogoutValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutValidator()
        {
            RuleFor(l => l.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token is required.");
        }
    }
}
