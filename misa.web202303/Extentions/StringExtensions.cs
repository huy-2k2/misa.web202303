namespace misa.web202303.Extentions
{
    public static class StringExtensions
    {
        public static string SnakeCaseToCamelCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var words = input.Split('_');
            var camelCaseWords = words.Select((word, index) =>
                index == 0 ? word.ToLower() : char.ToUpper(word[0]) + word.Substring(1).ToLower());

            var camelCaseString = string.Join("", camelCaseWords);
            return camelCaseString;
        }

        public static string SnakeCaseToPascalCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var words = input.Split('_');
            var pascalCaseWords = words.Select(word =>
                char.ToUpper(word[0]) + word.Substring(1).ToLower());

            var pascalCaseString = string.Join("", pascalCaseWords);
            return pascalCaseString;
        }
    }
}
