using System;

namespace Lanem.Loggers
{
    public static class Requires
    {
        public static void NotNull(object obj, string paramName = null)
        {
            if (obj == null)
                throw new ArgumentNullException(
                    paramName ?? nameof(obj));
        }

        public static void NotNullOrEmpty(string str, string paramName = null)
        {
            NotNull(str, paramName);

            if (str.Length == 0)
                throw new ArgumentException(
                    "Value cannot be empty.", 
                    paramName ?? nameof(str));
        }
    }
}