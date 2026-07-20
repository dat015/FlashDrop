using MediatR;
using FlashDrop.Identity.Api.Enums;

namespace FlashDrop.Identity.Application.Features.Users.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.Customer;
    }
}
