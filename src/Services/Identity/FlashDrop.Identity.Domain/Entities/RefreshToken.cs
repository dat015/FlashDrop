namespace FlashDrop.Identity.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; }

    public string TokenHash { get; private set; } = null!;

    public Guid UserId { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset ExpiresAt { get; private set; }

    public DateTimeOffset? RevokedAt { get; private set; }

    public User? User { get; private set; } = null!;

    private RefreshToken()
    {
    }

    public RefreshToken(
        string tokenHash,
        Guid userId,
        DateTimeOffset expiresAt)
    {
        Id = Guid.NewGuid();
        TokenHash = tokenHash;
        UserId = userId;
        CreatedAt = DateTimeOffset.UtcNow;
        ExpiresAt = expiresAt;
    }

    public bool IsExpired =>
        DateTimeOffset.UtcNow >= ExpiresAt;

    public bool IsRevoked =>
        RevokedAt.HasValue;

    public bool IsActive =>
        !IsExpired && !IsRevoked;

    public void Revoke()
    {
        if (IsRevoked)
        {
            return;
        }

        RevokedAt = DateTimeOffset.UtcNow;
    }
}