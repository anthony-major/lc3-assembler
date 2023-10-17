namespace Directives
{
    public class Stringz : Directive
    {
        public Stringz() : base(".stringz", 1)
        { }

        protected override void AssembleOperands(Assembler assembler)
        {
            if (!Util.IsString(assembler.CurrentLine[1]))
            {
                throw new Exception(ErrorMessages.ExpectedValidDirectiveOperand("string", "first", Name, assembler.CurrentLine));
            }

            for (var character = 1; character < assembler.CurrentLine[1].Length - 1; ++character)
            {
                assembler.AddWordToOutput(assembler.CurrentLine[1][character]);

                ++assembler.CurrentMemoryAddress;
            }

            assembler.AddWordToOutput(0);
            // The last character of the string (x0000) is added automatically at the end of our loop
            // since the default value for instruction is 0 and we don't mess with it here.
        }
    }
}