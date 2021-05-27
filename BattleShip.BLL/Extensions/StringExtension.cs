namespace BattleShip.BLL.Extensions
{
    /// <summary>
    /// String Extension Class
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Return first character of string
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static char FirstChar(this string stringValue)
        {
            return stringValue[0];
        }

        /// <summary>
        /// Convert string to uppercase
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static char FirstCharUpper(this string stringValue)
        {
            return char.ToUpper(stringValue.FirstChar());
        }

        /// <summary>
        /// Replace underscore present in string with space character
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static string ReplaceUnderscoreWithBlankSpace(this string stringValue)
        {
            return stringValue.Replace('_', ' ');
        }
    }
}