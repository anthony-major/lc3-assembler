namespace Directives
{
    public class Blkw : Directive
    {
        public Blkw() : base(".blkw", 1)
        { }

        protected override void AssembleOperands(Assembler assembler)
        {
            if (!Util.IsNumber(assembler.CurrentLine[1]))
            {
                throw new Exception(ErrorMessages.ExpectedValidDirectiveOperand("number", "first", Name, assembler.CurrentLine));
            }

            int wordCount = Util.GetNumber(assembler.CurrentLine[1]);

            if (wordCount < 0 || wordCount > 65535)
            {
                throw new Exception(ErrorMessages.InvalidDirectiveValue("data", Name, 0, 65535, assembler.CurrentLine));
            }

            for (var word = 0; word < wordCount; ++word)
            {
                assembler.AddWordToOutput(0);
            }

            assembler.CurrentMemoryAddress += (ushort)wordCount;
        }
    }
}