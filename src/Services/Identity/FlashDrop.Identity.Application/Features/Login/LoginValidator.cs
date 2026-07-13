using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Features.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Email).EmailAddress().NotEmpty();
            RuleFor(l => l.Password).NotEmpty().MinimumLength(8);
        }
    }
}
