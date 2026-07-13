using FlashDrop.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Abstractions.Authentication
{
    public interface IJwtTokenGenerator
    {
        string Generate(User user);
        string GenerateRefreshToken();
    }
}
