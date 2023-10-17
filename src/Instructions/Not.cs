namespace Instructions
{
    public class Not : Instruction
    {
        public Not() : base("not", 0b1001, 2)
        { }

        protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
        {
            Util.SetBits(ref instruction, Util.GetRegister(assembler.CurrentLine[1], assembler.CurrentLine), 3, 9);
            Util.SetBits(ref instruction, Util.GetRegister(assembler.CurrentLine[2], assembler.CurrentLine), 3, 6);
            Util.SetBits(ref instruction, 0b1, 1, 5);
            Util.SetBits(ref instruction, 0b11111, 5);
        }
    }
}