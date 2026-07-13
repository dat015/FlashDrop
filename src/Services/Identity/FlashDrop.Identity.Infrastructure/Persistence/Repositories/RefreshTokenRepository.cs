using System;
using System.Collections.Generic;
using System.Text;
using FlashDrop.Identity.Application.Abstractions.Persistence;
using FlashDrop.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlashDrop.Identity.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentityDbContext _context;
        public RefreshTokenRepository(IdentityDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(x => string.Equals(x.TokenHash, tokenHash), cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            _context.RefreshTokens.Update(refreshToken);
        }
    }
}
