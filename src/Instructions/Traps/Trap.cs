namespace Instructions
{
    namespace Traps
    {
        public abstract class Trap : Instruction
        {
            protected Trap(string name) : base(name, 0b1111, 0)
            { }
        }
    }
}