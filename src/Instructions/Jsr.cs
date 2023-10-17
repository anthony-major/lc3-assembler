namespace Instructions
{
    public class Jsr : Instruction
    {
        public Jsr() : base("jsr", 0b0100, 1)
        { }

        protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
        {
            if (!assembler.SymbolTable.ContainsKey(assembler.CurrentLine[1]))
            {
                throw new Exception(ErrorMessages.ExpectedValidOperand("label", "first", Name, assembler.CurrentLine));
            }

            Util.SetBits(ref instruction, 0b1, 1, 11);

            int programCounterOffset = assembler.SymbolTable[assembler.CurrentLine[1]] - (assembler.CurrentMemoryAddress + 1);

            if (!Util.IsInBitRange(programCounterOffset, 11))
            {
                throw new Exception(ErrorMessages.InvalidValue("program counter offset", Name, -1024, 1023, assembler.CurrentLine));
            }

            Util.SetBits(ref instruction, (ushort)programCounterOffset, 11);
        }
    }
}