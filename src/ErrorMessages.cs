public static class ErrorMessages
{
    public static string InvalidValue(string valueType, string instruction, int minValue, int maxValue, List<string> line)
    {
        return $"Invalid {valueType} value for instruction '{instruction}'. Value must be between {minValue} and {maxValue} inclusive.\n{GetLineMessage(line)}";
    }

    public static string InvalidOperandCount(string instruction, int expectedOperandCount, List<string> line)
    {
        return $"Invalid number of operands for instruction '{instruction}'. '{instruction}' expects {expectedOperandCount} operands.\n{GetLineMessage(line)}";
    }

    public static string InvalidRegister(string token, List<string> line)
    {
        return $"Token '{token}' is not a valid register.\n{GetLineMessage(line)}";
    }

    public static string ExpectedValidOperand(string valueType, string operandPosition, string instruction, List<string> line)
    {
        return $"Expected valid {valueType} as {operandPosition} operand for instruction '{instruction}'.\n{GetLineMessage(line)}";
    }

    public static string ExpectedValidDirectiveOperand(string valueType, string operandPosition, string directive, List<string> line)
    {
        return $"Expected valid {valueType} as {operandPosition} operand for directive '{directive}'.\n{GetLineMessage(line)}";
    }

    public static string InvalidDirectiveValue(string valueType, string directive, int minValue, int maxValue, List<string> line)
    {
        return $"Invalid {valueType} value for directive '{directive}'. Value must be between {minValue} and {maxValue} inclusive.\n{GetLineMessage(line)}";
    }

    public static string OutOfMemory(List<string> line)
    {
        return $"Out of memory locations.\n{GetLineMessage(line)}";
    }

    public static string UnknownToken(List<string> line)
    {
        return $"Unknown token '{line[0]}'.\n{GetLineMessage(line)}";
    }

    private static string GetLineMessage(List<string> line)
    {
        return $"Line: {string.Join(' ', line)}";
    }
}