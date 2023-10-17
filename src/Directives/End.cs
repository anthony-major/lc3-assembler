namespace Directives
{
    public class End : Directive
    {
        public End() : base(".end", 0)
        { }

        protected override void AssembleOperands(Assembler assembler)
        { }
    }
}