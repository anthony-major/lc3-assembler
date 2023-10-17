namespace Instructions
{
    namespace Traps
    {
        public class Getc : Trap
        {
            public Getc() : base("getc")
            { }

            protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
            {
                Util.SetBits(ref instruction, 0x20, 8);
            }
        }
    }
}