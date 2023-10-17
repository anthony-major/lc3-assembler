namespace Instructions
{
    public class Ret : Instruction
    {
        public Ret() : base("ret", 0b1100, 0)
        { }

        protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
        {
            Util.SetBits(ref instruction, 0b111, 3, 6);
        }
    }
}