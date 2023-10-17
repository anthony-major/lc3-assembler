namespace Instructions
{
    namespace Traps
    {
        public class Halt : Trap
        {
            public Halt() : base("halt")
            { }

            protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
            {
                Util.SetBits(ref instruction, 0x25, 8);
            }
        }
    }
}