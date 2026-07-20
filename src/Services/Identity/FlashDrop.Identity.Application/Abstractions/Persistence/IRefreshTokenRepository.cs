using FlashDrop.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Abstractions.Persistence
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
        Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken);
        Task Update(RefreshToken refreshToken, CancellationToken cancellationToken);
        Task DeleteAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
