namespace Instructions
{
    namespace Traps
    {
        public class Puts : Trap
        {
            public Puts() : base("puts")
            { }

            protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
            {
                Util.SetBits(ref instruction, 0x22, 8);
            }
        }
    }
}