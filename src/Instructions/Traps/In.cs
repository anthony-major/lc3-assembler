namespace Instructions
{
    namespace Traps
    {
        public class In : Trap
        {
            public In() : base("in")
            { }

            protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
            {
                Util.SetBits(ref instruction, 0x23, 8);
            }
        }
    }
}