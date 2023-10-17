namespace Instructions
{
    public class Add : Instruction
    {
        public Add() : base("add", 0b0001, 3)
        { }

        protected override void AssembleOperands(Assembler assembler, ref ushort instruction)
        {
            Util.SetBits(ref instruction, Util.GetRegister(assembler.CurrentLine[1], assembler.CurrentLine), 3, 9);
            Util.SetBits(ref instruction, Util.GetRegister(assembler.CurrentLine[2], assembler.CurrentLine), 3, 6);

            if (Util.IsRegister(assembler.CurrentLine[3]))
            {
                Util.SetBits(ref instruction, Util.GetRegister(assembler.CurrentLine[3], assembler.CurrentLine), 3);
            }
            else if (Util.IsNumber(assembler.CurrentLine[3]))
            {
                int immediateValue = Util.GetNumber(assembler.CurrentLine[3]);

                if (!Util.IsInBitRange(immediateValue, 5))
                {
                    throw new Exception(ErrorMessages.InvalidValue("immediate", Name, -16, 15, assembler.CurrentLine));
                }

                Util.SetBits(ref instruction, 0b1, 1, 5);
                Util.SetBits(ref instruction, (ushort)immediateValue, 5);
            }
            else
            {
                throw new Exception(ErrorMessages.ExpectedValidOperand("register or number", "third", Name, assembler.CurrentLine));
            }
        }
    }
}