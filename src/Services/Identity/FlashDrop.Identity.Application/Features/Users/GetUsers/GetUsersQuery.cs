using MediatR;
using System.Collections.Generic;

namespace FlashDrop.Identity.Application.Features.Users.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<GetUsersResponse>>
    {
    }
}
