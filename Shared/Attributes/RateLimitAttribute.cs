using System;

namespace FlashDrop.Shared.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class RateLimitAttribute : Attribute
{
    public int PermitLimit { get; }

    public TimeSpan Window { get; }

    public RateLimitAttribute(
        int permitLimit,
        int windowInMinutes = 1)
    {
        PermitLimit = permitLimit;
        Window = TimeSpan.FromMinutes(windowInMinutes);
    }
}