namespace Framework.Extensions
{
    /// <summary>
    /// Contains string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Removes charaters not usable in neos while parsing the string.
        /// </summary>
        /// <param name="text">The text on which to remove that characters.</param>
        /// <returns>A string stripped of all the unwanted characters.</returns>
        public static string JsonCleanUp(this string text)
        {
            var retval = text;

            retval = retval.Replace("\"", "");

            return retval;
        }
    }
}