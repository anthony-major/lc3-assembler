namespace Instructions
{
    public class Trap : Instruction
    {
        public Trap() : base("trap", 0b1111, 1)
        { }

        protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
        {
            if (!Util.IsNumber(assembler.CurrentLine[1]))
            {
                throw new Exception(ErrorMessages.ExpectedValidOperand("number", "first", Name, assembler.CurrentLine));
            }

            int trapVector = Util.GetNumber(assembler.CurrentLine[1]);

            if (!Util.IsInBitRange(trapVector, 8))
            {
                throw new Exception(ErrorMessages.InvalidValue("vector", Name, -128, 127, assembler.CurrentLine));
            }

            Util.SetBits(ref instruction, (ushort)trapVector, 8);
        }
    }
}