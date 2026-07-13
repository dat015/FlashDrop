using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Infrastructure.Authentication
{
    public sealed class JwtSettings
    {
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public string SecretKey { get; init; } = default!;
        public int ExpirationMinutes { get; init; }
    }
}
