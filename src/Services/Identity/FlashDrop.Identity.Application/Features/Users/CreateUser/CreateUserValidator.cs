using FluentValidation;

namespace FlashDrop.Identity.Application.Features.Users.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Role).IsInEnum();
        }
    }
}
