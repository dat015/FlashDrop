using System;
using System.Collections.Generic;
using System.Text;
using FlashDrop.Identity.Application.Abstractions.Security;
namespace FlashDrop.Identity.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
