public class AssemblyLine
{
    public List<string> Tokens { get; } = new List<string>();

    private string originalLine = "";
    private int currentLineIndex = -1;

    public AssemblyLine(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            return;
        }

        originalLine = line;
        Tokenize();
    }

    public override string ToString()
    {
        return string.Join(' ', Tokens);
    }

    private void Tokenize()
    {
        while (++currentLineIndex < originalLine.Length)
        {
            string? token = GetNextToken();

            // End of line
            if (token == null)
            {
                break;
            }
            // Skip empty tokens
            if (token == "")
            {
                continue;
            }

            Tokens.Add(token);
        }
    }

    private string? GetNextToken()
    {
        string token = "";

        while (currentLineIndex < originalLine.Length)
        {
            char currentCharacter = originalLine[currentLineIndex];

            if (currentCharacter == ';')
            {
                return null;
            }
            else if (currentCharacter == '"')
            {
                return GetString();
            }
            else if (IsStopCharacter(currentCharacter))
            {
                return token;
            }

            token += currentCharacter;
            ++currentLineIndex;
        }

        return token;
    }

    private bool IsStopCharacter(char character)
    {
        return character == ' ' || character == ',' || character == '\n';
    }

    private string GetString()
    {
        string token = "\"";

        ++currentLineIndex;

        while (currentLineIndex < originalLine.Length)
        {
            char currentCharacter = originalLine[currentLineIndex];

            token += currentCharacter;

            if (currentCharacter == '"')
            {
                return token;
            }

            ++currentLineIndex;
        }

        return token;
    }
}