namespace SteganographyTool
{
    public static class RemoveWhitespace
    {
        public static string Clean(string input)
        {
            bool done = false;
            while (done == false) //verwijder begintekens
            {
                if (input.IndexOf("\n") == 0)
                {
                    input = input.Substring("\n".Length);
                }
                else if (input.IndexOf("\r\n") == 0)
                {
                    input = input.Substring("\r\n".Length);
                }
                else if (input.IndexOf(" ") == 0)
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
                    if (input.LastIndexOf("\r\n") == length - 2 && length > 1)
                    {
                        input = input.Substring(0, length - "\r\n".Length);
                    }
                    else if (input.LastIndexOf("\n") == length - 1)
                    {
                        input = input.Substring(0, length - "\n".Length);
                    }
                    else if (input.LastIndexOf(" ") == length - 1)
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