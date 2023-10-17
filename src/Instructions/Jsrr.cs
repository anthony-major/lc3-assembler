namespace Instructions
{
    public class Jsrr : Instruction
    {
        public Jsrr() : base("jsrr", 0b0100, 1)
        { }

        protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
        {
            Util.SetBits(ref instruction, Util.GetRegister(assembler.CurrentLine[1], assembler.CurrentLine), 3, 6);
        }
    }
}