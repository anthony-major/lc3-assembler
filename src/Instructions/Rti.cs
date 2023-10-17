namespace Instructions
{
    public class Rti : Instruction
    {
        public Rti() : base("rti", 0b1000, 0)
        { }

        protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
        { }
    }
}