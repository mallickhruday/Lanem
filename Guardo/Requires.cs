using System;

namespace Guardo
{
    /// <summary>
    /// Defines methods to validate method arguments.
    /// </summary>
    public static class Requires
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given object is null.
        /// </summary>
        /// <param name="obj">The object to be validated.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if the given object is null.</exception>
        public static void NotNull(object obj, string paramName = null)
        {
            if (obj == null)
                throw new ArgumentNullException(
                    paramName ?? nameof(obj));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given string is null or an <see cref="ArgumentException"/> if the string is empty.
        /// </summary>
        /// <param name="str">The string to be validated.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown if the given string is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the given string is empty.</exception>
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