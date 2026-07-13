using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace FlashDrop.Identity.Application.Features.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(8);
        }

    }
}
