namespace Instructions
{
    public abstract class Instruction
    {
        public string Name { get; }
        public byte Opcode { get; }
        public byte OperandCount { get; }

        protected Instruction(string name, byte opcode, byte operandCount)
        {
            Name = name;
            Opcode = opcode;
            OperandCount = operandCount;
        }

        public ushort Assemble(Assembler assembler)
        {
            ushort instruction = 0;

            Util.CheckOperandCount(assembler.CurrentLine, OperandCount);
            Util.SetBits(ref instruction, Opcode, 4, 12);
            AssembleOperands(assembler, ref instruction);

            return instruction;
        }

        protected abstract void AssembleOperands(Assembler assembler, ref ushort instruction);
    }
}