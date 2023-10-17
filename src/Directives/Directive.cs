namespace Directives
{
    public abstract class Directive
    {
        public string Name { get; }
        public byte OperandCount { get; }

        protected Directive(string name, byte operandCount)
        {
            Name = name;
            OperandCount = operandCount;
        }

        public void Assemble(Assembler assembler)
        {
            Util.CheckOperandCount(assembler.CurrentLine, OperandCount);
            AssembleOperands(assembler);
        }

        protected abstract void AssembleOperands(Assembler assembler);
    }
}