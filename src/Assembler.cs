public class Assembler
{
    public List<byte> Output { get; } = new();
    public Dictionary<string, ushort> SymbolTable { get; } = new();
    public List<string> CurrentLine { get; private set; } = new();
    public ushort CurrentMemoryAddress
    {
        get
        {
            return currentMemoryAddress;
        }
        set
        {
            currentMemoryAddress = value;

            if (currentMemoryAddress > ushort.MaxValue)
            {
                throw new Exception(ErrorMessages.OutOfMemory(CurrentLine));
            }
        }
    }

    private const ushort DefaultOriginAddress = 0x3000;

    private Assembly assembly;
    private string outputFilePath;

    private ushort currentMemoryAddress;

    private Dictionary<string, Instructions.Instruction> instructions = new();
    private Dictionary<string, Directives.Directive> directives = new();

    public Assembler(string assemblyFilePath, string outputFilePath)
    {
        assembly = new Assembly(assemblyFilePath);
        this.outputFilePath = outputFilePath;

        Instructions.Instruction[] instructionInstances =
        {
            new Instructions.Add(),
            new Instructions.And(),
            new Instructions.Br(),
            new Instructions.Jmp(),
            new Instructions.Jsr(),
            new Instructions.Jsrr(),
            new Instructions.Ld(),
            new Instructions.Ldi(),
            new Instructions.Ldr(),
            new Instructions.Lea(),
            new Instructions.Not(),
            new Instructions.Ret(),
            new Instructions.Rti(),
            new Instructions.St(),
            new Instructions.Sti(),
            new Instructions.Str(),
            new Instructions.Trap()
        };
        Instructions.Traps.Trap[] trapInstances =
        {
            new Instructions.Traps.Getc(),
            new Instructions.Traps.Halt(),
            new Instructions.Traps.In(),
            new Instructions.Traps.Out(),
            new Instructions.Traps.Puts(),
            new Instructions.Traps.Putsp()
        };
        Directives.Directive[] directiveInstances =
        {
            new Directives.Blkw(),
            new Directives.End(),
            new Directives.Fill(),
            new Directives.Stringz()
        };

        foreach (var instance in instructionInstances)
        {
            instructions[instance.Name] = instance;
        }
        foreach (var instance in trapInstances)
        {
            instructions[instance.Name] = instance;
        }
        foreach (var instance in directiveInstances)
        {
            directives[instance.Name] = instance;
        }
        // Add the other breaks instructions
        instructions["brn"] = instructionInstances[2];
        instructions["brz"] = instructionInstances[2];
        instructions["brp"] = instructionInstances[2];
        instructions["brzp"] = instructionInstances[2];
        instructions["brnp"] = instructionInstances[2];
        instructions["brnz"] = instructionInstances[2];
        instructions["brnzp"] = instructionInstances[2];
    }

    public void Assemble()
    {
        if (assembly.Lines.Count == 0)
        {
            File.WriteAllBytes(outputFilePath, Output.ToArray());
            return;
        }

        ushort originAddress;
        try
        {
            originAddress = GetOrigin();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error getting origin address: \"{e.Message}\"");
            return;
        }

        AddWordToOutput(originAddress);

        // // Testing
        // Console.WriteLine("Origin: " + Convert.ToString(originAddress, 16));
        // Console.WriteLine();

        // Console.WriteLine("Assembly before first pass:");
        // Console.WriteLine(assembly.ToString());
        // ////////

        CurrentMemoryAddress = originAddress;
        FirstPass();
        CurrentMemoryAddress = originAddress;
        try
        {
            SecondPass();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error assembling: \"{e.Message}\"");
            return;
        }

        File.WriteAllBytes(outputFilePath, Output.ToArray());

        // // Testing
        // Console.WriteLine("Symbol table:");
        // foreach (var entry in SymbolTable)
        // {
        //     Console.WriteLine(entry.ToString());
        // }
        // Console.WriteLine();

        // Console.WriteLine("Assembly after first pass:");
        // Console.WriteLine(assembly.ToString());
        // ////////
    }

    public void AddWordToOutput(ushort word)
    {
        Output.Add((byte)(word >> 8));
        Output.Add((byte)Util.ExtractBits(word, 8));
    }

    private ushort GetOrigin()
    {
        if (assembly.Lines[0][0] == ".orig")
        {
            if (assembly.Lines[0].Count < 2 || !Util.IsNumber(assembly.Lines[0][1]))
            {
                throw new Exception($"Invalid number given for origin directive.");
            }

            int originAddress = Util.GetNumber(assembly.Lines[0][1]);

            if (!Util.IsInRange(originAddress, 0, ushort.MaxValue))
            {
                throw new Exception($"Origin address '{originAddress}' is out of range. Value must be betwwen 0 and {ushort.MaxValue}.");
            }

            // We have the origin now, so we can just remove the first assembly line.
            assembly.Lines.RemoveAt(0);

            return (ushort)originAddress;
        }

        return DefaultOriginAddress;
    }

    private void FirstPass()
    {
        List<string> labels = new List<string>();

        for (var lineIndex = 0; lineIndex < assembly.Lines.Count; ++lineIndex)
        {
            var line = assembly.Lines[lineIndex];

            for (var tokenIndex = 0; tokenIndex < line.Count; ++tokenIndex)
            {
                var token = line[tokenIndex];

                if (Util.IsLabel(token))
                {
                    labels.Add(token);
                    // Remove label from assembly code. This makes things easier later. (We will not have to check if a token is a label.)
                    line.RemoveAt(tokenIndex--); // '--' is so we don't skip tokens

                    // Remove lines that end up empty
                    if (line.Count == 0)
                    {
                        assembly.Lines.RemoveAt(lineIndex--); // '--' is so we don't skip lines
                        break; // We are done with this line, so break
                    }
                }
                else
                {
                    foreach (var label in labels)
                    {
                        if (SymbolTable.ContainsKey(label))
                        {
                            continue;
                        }

                        SymbolTable.Add(label, CurrentMemoryAddress);
                    }

                    labels.Clear();
                    ++CurrentMemoryAddress;

                    break;
                }
            }
        }
    }

    private void SecondPass()
    {
        foreach (var line in assembly.Lines)
        {
            CurrentLine = line;

            if (instructions.ContainsKey(line[0]))
            {
                ushort instruction = instructions[line[0]].Assemble(this);

                AddWordToOutput(instruction);
            }
            else if (directives.ContainsKey(line[0]))
            {
                directives[line[0]].Assemble(this);
            }
            else
            {
                throw new Exception(ErrorMessages.UnknownToken(CurrentLine));
            }

            ++CurrentMemoryAddress;
        }
    }

    private void CheckOperandCount(List<string> assemblyLine, int expectedOperandCount)
    {
        if (assemblyLine.Count != (expectedOperandCount + 1))
        {
            throw new Exception($"Invalid number of operands. Opcode '{assemblyLine[0]}' expects {expectedOperandCount} operands. Line: {String.Join(", ", assemblyLine)}");
        }
    }

    private ushort GetRegister(List<string> assemblyLine, string token)
    {
        if (!Util.IsRegister(token))
        {
            throw new Exception($"Token '{token}' is not a valid register. Line: {String.Join(", ", assemblyLine)}");
        }

        return (ushort)(token[1] - '0');
    }
}