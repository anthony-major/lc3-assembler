if (args.Length < 1)
{
    Console.WriteLine("An input file must be specified.");
    return;
}

Assembler assembler = new Assembler(args[0], args.Length < 2 ? "out.obj" : args[1]);

assembler.Assemble();

// FOR DEBUG
// Assembler assembler = new Assembler("assembly/test.asm", "out.obj");
// assembler.Assemble();