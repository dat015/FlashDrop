using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Abstractions.Authentication
{
    public interface IRefreshTokenHasher
    {
        string Hash(string refreshToken);
    }
}
