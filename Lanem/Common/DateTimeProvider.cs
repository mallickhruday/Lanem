using System;

namespace Lanem.Parsers
{
    public sealed class DateTimeProvider
    {
        private static DateTime? _utcNow;

        public static void SetUtcNow(DateTime utcNow)
        {
            _utcNow = utcNow;
        }

        public static DateTime UtcNow => _utcNow ?? DateTime.UtcNow;
    }
}