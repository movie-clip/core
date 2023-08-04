using System;

namespace Core.Data.Time
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}