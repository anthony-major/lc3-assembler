public static class Util
{
    private static string[] opcodes =
    {
        "add",
        "and",
        "br", "brn", "brz", "brp", "brzp", "brnp", "brnz", "brnzp",
        "jmp",
        "jsr", "jsrr",
        "ld",
        "ldi",
        "ldr",
        "lea",
        "not",
        "ret",
        "rti",
        "st",
        "sti",
        "str",
        "trap"
    };

    private static string[] trapCodes =
    {
        "getc",
        "out",
        "puts",
        "in",
        "putsp",
        "halt"
    };

    public static bool IsNumber(string token)
    {
        token = token.ToLower();

        try
        {
            GetNumber(token);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public static int GetNumber(string token)
    {
        token = token.ToLower();

        if (token.StartsWith('#'))
        {
            return GetDecimalNumber(token.Substring(1));
        }
        else if (token.StartsWith('x'))
        {
            return GetHexNumber(token.Substring(1));
        }
        else if (token.StartsWith('b'))
        {
            return GetBinaryNumber(token.Substring(1));
        }
        else
        {
            throw new Exception($"Token '{token}' is not a valid LC3 number.");
        }
    }

    public static void SetBits(ref ushort data, ushort bits, ushort maxBits, ushort start = 0)
    {
        bits = ExtractBits(bits, maxBits);
        bits <<= start;

        data |= bits;
    }

    public static ushort ExtractBits(ushort data, ushort count, ushort start = 0)
    {
        ushort mask = (ushort)((1 << count) - 1);

        data >>= start;
        data &= mask;

        return data;
    }

    public static bool IsLabel(string token)
    {
        return !(IsOpcode(token) || IsRegister(token) || IsTrapCode(token) || IsDirective(token) || IsNumber(token));
    }

    public static bool IsOpcode(string token)
    {
        token = token.ToLower();

        return opcodes.Contains(token);
    }

    public static bool IsRegister(string token)
    {
        token = token.ToLower();

        return token.Length == 2 && token[0] == 'r' && token[1] >= '0' && token[1] <= '7';
    }

    public static bool IsTrapCode(string token)
    {
        token = token.ToLower();

        return trapCodes.Contains(token);
    }

    public static bool IsDirective(string token)
    {
        return token.StartsWith('.');
    }

    public static bool IsString(string token)
    {
        return token.StartsWith('"') && token.EndsWith('"');
    }

    public static bool IsInBitRange(int number, int bitCount)
    {
        int max = (1 << bitCount) - 1;
        int min = -(max / 2) - 1;

        return number >= min && number <= max;
    }

    public static bool IsInRange(int number, int min, int max)
    {
        return number >= min && number <= max;
    }

    public static void CheckOperandCount(List<string> assemblyLine, int expectedOperandCount)
    {
        if ((assemblyLine.Count - 1) != expectedOperandCount)
        {
            throw new Exception(ErrorMessages.InvalidOperandCount(assemblyLine[0], expectedOperandCount, assemblyLine));
        }
    }

    public static ushort GetRegister(string token, List<string> assemblyLine)
    {
        if (!Util.IsRegister(token))
        {
            throw new Exception(ErrorMessages.InvalidRegister(token, assemblyLine));
        }

        return (ushort)(token[1] - '0');
    }

    private static int GetDecimalNumber(string numberString)
    {
        return Convert.ToInt32(numberString);
    }

    private static int GetHexNumber(string numberString)
    {
        return Convert.ToInt32(numberString, 16);
    }

    private static int GetBinaryNumber(string numberString)
    {
        return Convert.ToInt32(numberString, 2);
    }
}