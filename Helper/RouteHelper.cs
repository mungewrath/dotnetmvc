using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace dotnetmvc.Helper
{
    public static class RouteHelper
    {
        public static string Format(string format, params Object[] args)
        {
            if (IsProperFormatString(format))
            {
                format = MakeFormatSafeForStringFormat(format);
                return String.Format(CultureInfo.InvariantCulture, format, args);
            }

            return "";
        }

        private static bool IsProperFormatString(string format)
        {
            bool inBrackets = false;
            foreach (char c in format)
            {
                if (c == '{' && !inBrackets)
                {
                    inBrackets = true;
                }
                else if (c == '{')
                {
                    return false;
                }
                else if (c == '}')
                {
                    inBrackets = false;
                }
            }

            if (inBrackets)
            {
                return false;
            }

            return true;
        }

        private static string MakeFormatSafeForStringFormat(string format)
        {
            string emptyBrackets = Regex.Replace(format, @"\{[^}]*\}", "{");
            string[] tokens = emptyBrackets.Split('{');

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < tokens.Length - 1; ++i)
            {
                sb.Append(tokens[i].ToCharArray());
                sb.Append(BuildFormatOption(i));
            }

            sb.Append(tokens[tokens.Length - 1]);

            return sb.ToString();
        }

        private static string BuildFormatOption(int i)
        {
            return "{" + i + "}";
        }
    }
}
