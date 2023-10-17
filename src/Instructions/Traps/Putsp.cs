namespace Instructions
{
    namespace Traps
    {
        public class Putsp : Trap
        {
            public Putsp() : base("putsp")
            { }

            protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
            {
                Util.SetBits(ref instruction, 0x24, 8);
            }
        }
    }
}