using FluentValidation;

namespace FlashDrop.Identity.Application.Features.Users.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Role).IsInEnum();
        }
    }
}
