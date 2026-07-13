using FlashDrop.Identity.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace FlashDrop.Identity.Infrastructure.Authentication
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid? UserId
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                return Guid.TryParse(id, out var guid)
                    ? guid
                    : null;
            }
        }

        public string? Email =>
            _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue(ClaimTypes.Email);

        public string? Username =>
            _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue(ClaimTypes.Name);

        public string? Role =>
            _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue(ClaimTypes.Role);
    }
}
