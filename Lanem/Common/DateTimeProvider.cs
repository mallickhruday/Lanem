using System;

namespace Lanem.Common
{
    /// <summary>
    /// Wraps static members from the <see cref="DateTime"/> class with the possibility to override values from a test context.
    /// </summary>
    public static class DateTimeProvider
    {
        private static DateTime? _utcNow;

        public static void SetUtcNow(DateTime utcNow)
        {
            _utcNow = utcNow;
        }

        public static void Reset()
        {
            _utcNow = null;
        }

        public static DateTime UtcNow => _utcNow ?? DateTime.UtcNow;
    }
}