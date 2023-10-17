namespace Instructions
{
    public class Ldr : Instruction
    {
        public Ldr() : base("ldr", 0b0110, 3)
        { }

        protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
        {
            if (!Util.IsNumber(assembler.CurrentLine[3]))
            {
                throw new Exception(ErrorMessages.ExpectedValidOperand("number", "third", Name, assembler.CurrentLine));
            }

            Util.SetBits(ref instruction, Util.GetRegister(assembler.CurrentLine[1], assembler.CurrentLine), 3, 9);
            Util.SetBits(ref instruction, Util.GetRegister(assembler.CurrentLine[2], assembler.CurrentLine), 3, 6);

            int offset = Util.GetNumber(assembler.CurrentLine[3]);

            if (!Util.IsInBitRange(offset, 6))
            {
                throw new Exception(ErrorMessages.InvalidValue("offset", Name, -32, 31, assembler.CurrentLine));
            }

            Util.SetBits(ref instruction, (ushort)offset, 6);
        }
    }
}