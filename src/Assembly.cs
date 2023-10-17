public class Assembly
{
    public List<List<string>> Lines { get; } = new List<List<string>>();

    public Assembly(string assemblyFilePath)
    {
        string[] assemblyLines = File.ReadAllLines(assemblyFilePath);

        Tokenize(assemblyLines);
        ConvertLinesToLowercase();
    }

    public override string ToString()
    {
        string assemblyString = "";

        foreach (var line in Lines)
        {
            foreach (var token in line)
            {
                assemblyString += $"{token} ";
            }
            assemblyString += "\n";
        }

        return assemblyString;
    }

    private void Tokenize(string[] assemblyLines)
    {
        foreach (var assemblyLine in assemblyLines)
        {
            AssemblyLine line = new AssemblyLine(assemblyLine);

            // We do not want to add empty lines
            if (line.Tokens.Count <= 0)
            {
                continue;
            }

            Lines.Add(line.Tokens);
        }
    }

    private void ConvertLinesToLowercase()
    {
        for (var line = 0; line < Lines.Count; ++line)
        {
            for (var token = 0; token < Lines[line].Count; ++token)
            {
                // We do not want to lowercase strings
                if (Lines[line][token].StartsWith('"'))
                {
                    continue;
                }

                Lines[line][token] = Lines[line][token].ToLower();
            }
        }
    }
}