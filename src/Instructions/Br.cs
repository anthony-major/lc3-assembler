namespace Instructions
{
    public class Br : Instruction
    {
        public Br() : base("br", 0b0000, 1)
        { }

        protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
        {
            if (!assembler.SymbolTable.ContainsKey(assembler.CurrentLine[1]))
            {
                throw new Exception(ErrorMessages.ExpectedValidOperand("label", "first", assembler.CurrentLine[0], assembler.CurrentLine));
            }

            if (assembler.CurrentLine[0] == "br")
            {
                assembler.CurrentLine[0] += "nzp";
            }

            if (assembler.CurrentLine[0].Contains('n'))
            {
                Util.SetBits(ref instruction, 0b1, 1, 11);
            }
            if (assembler.CurrentLine[0].Contains('z'))
            {
                Util.SetBits(ref instruction, 0b1, 1, 10);
            }
            if (assembler.CurrentLine[0].Contains('p'))
            {
                Util.SetBits(ref instruction, 0b1, 1, 9);
            }

            int programCounterOffset = assembler.SymbolTable[assembler.CurrentLine[1]] - (assembler.CurrentMemoryAddress + 1);

            if (!Util.IsInBitRange(programCounterOffset, 9))
            {
                throw new Exception(ErrorMessages.InvalidValue("program counter offset", assembler.CurrentLine[0], -256, 255, assembler.CurrentLine));
            }

            Util.SetBits(ref instruction, (ushort)programCounterOffset, 9);
        }
    }
}