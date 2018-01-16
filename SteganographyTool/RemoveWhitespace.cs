using System;

namespace SteganographyTool
{
    public static class RemoveWhitespace
    {
        public static string Clean(string input)
        {
            bool done = false;
            while (done == false) //verwijder begintekens
            {
                if (input.IndexOf("\n", StringComparison.Ordinal) == 0)
                {
                    input = input.Substring("\n".Length);
                }
                else if (input.IndexOf("\r\n", StringComparison.Ordinal) == 0)
                {
                    input = input.Substring("\r\n".Length);
                }
                else if (input.IndexOf(" ", StringComparison.Ordinal) == 0)
                {
                    input = input.Substring(" ".Length);
                }
                else
                {
                    done = true;
                }
            }

            done = false;
            while (done == false) //verwijder eindtekens
            {
                int length = input.Length;
                if (length > 0)
                {
                    if (input.LastIndexOf("\r\n", StringComparison.Ordinal) == length - 2 && length > 1)
                    {
                        input = input.Substring(0, length - "\r\n".Length);
                    }
                    else if (input.LastIndexOf("\n", StringComparison.Ordinal) == length - 1)
                    {
                        input = input.Substring(0, length - "\n".Length);
                    }
                    else if (input.LastIndexOf(" ", StringComparison.Ordinal) == length - 1)
                    {
                        input = input.Substring(0, length - " ".Length);
                    }
                    else
                    {
                        done = true;
                    }
                }
                else
                {
                    done = true;
                }
            }
            return input;
        }
    }
}