namespace Directives
{
    public class Fill : Directive
    {
        public Fill() : base(".fill", 1)
        { }

        protected override void AssembleOperands(Assembler assembler)
        {
            int value;

            if (Util.IsNumber(assembler.CurrentLine[1]))
            {
                value = Util.GetNumber(assembler.CurrentLine[1]);
            }
            else if (Util.IsLabel(assembler.CurrentLine[1]))
            {
                value = assembler.SymbolTable[assembler.CurrentLine[1]];
            }
            else
            {
                throw new Exception(ErrorMessages.ExpectedValidDirectiveOperand("number or label", "first", Name, assembler.CurrentLine));
            }

            if (!Util.IsInBitRange(value, 16))
            {
                throw new Exception(ErrorMessages.InvalidDirectiveValue("data", Name, -32768, 32767, assembler.CurrentLine));
            }

            assembler.AddWordToOutput((ushort)value);
        }
    }
}