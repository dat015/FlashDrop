using MediatR;
using System;

namespace FlashDrop.Identity.Application.Features.Users.GetUser
{
    public class GetUserQuery : IRequest<GetUserResponse>
    {
        public Guid Id { get; set; }
        public GetUserQuery(Guid id)
        {
            Id = id;
        }
    }
}
