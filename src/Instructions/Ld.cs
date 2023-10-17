namespace Instructions
{
    public class Ld : Instruction
    {
        public Ld() : base("ld", 0b0010, 2)
        { }

        protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
        {
            if (!assembler.SymbolTable.ContainsKey(assembler.CurrentLine[2]))
            {
                throw new Exception(ErrorMessages.ExpectedValidOperand("label", "second", Name, assembler.CurrentLine));
            }

            Util.SetBits(ref instruction, Util.GetRegister(assembler.CurrentLine[1], assembler.CurrentLine), 3, 9);

            int programCounterOffset = assembler.SymbolTable[assembler.CurrentLine[2]] - (assembler.CurrentMemoryAddress + 1);

            if (!Util.IsInBitRange(programCounterOffset, 9))
            {
                throw new Exception(ErrorMessages.InvalidValue("program counter offset", Name, -256, 255, assembler.CurrentLine));
            }

            Util.SetBits(ref instruction, (ushort)programCounterOffset, 9);
        }
    }
}