namespace Instructions
{
    namespace Traps
    {
        public class Out : Trap
        {
            public Out() : base("out")
            { }

            protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
            {
                Util.SetBits(ref instruction, 0x21, 8);
            }
        }
    }
}