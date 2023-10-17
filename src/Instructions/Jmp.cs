namespace Instructions
{
    public class Jmp : Instruction
    {
        public Jmp() : base("jmp", 0b1100, 1)
        { }

        protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
        {
            Util.SetBits(ref instruction, Util.GetRegister(assembler.CurrentLine[1], assembler.CurrentLine), 3, 6);
        }
    }
}