using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Abstractions.Authentication
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Email { get; }
        string? Username { get; }
        string? Role { get; }

    }
}
