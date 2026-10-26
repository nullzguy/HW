using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml;

namespace HW.Classes;

public class TUI
{
    public static void Run()
    {
        int ? width =  Console.WindowWidth;
        int ? height = Console.WindowHeight;

        Console.WriteLine($" -< Nullz's country economics simulator >-");
        Console.WriteLine($"'h' for a list of commands\n" +
                          $"'q' to quit");

        bool q = false;
        while (q == false)
        {
            Console.Write(" > ");
            string ? input = Console.ReadLine();
            try
            {
                var args = TokenizeInput(input);
                RunCommand(args, ref q);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                continue;
            }

            //RunCommand(args);
        }
    }

    private static string[] TokenizeInput(string input)
    {
        var input0 = input.Trim();
        if (input0 == "")
            throw new("~Nya! T-the input i-is empty!!~");
        return input0.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    private static void RunCommand(string[] args, ref bool q)
    {
        // Help
        if (args[0] == "h" || args[0] == "help")
        {
            if (args.Length == 1)
            {
                RunHelp();
                return;
            }
            else
                throw new("~Nya! T-too many a-args!!~");
        }

        // Quit
        if (args[0] == "q" || args[0] == "quit")
        {
            if (args.Length == 1)
            {
                q = true;
                return;
            }
            else
                throw new("~Nya! T-too many a-args!!~");
        }

        // Clear
        if (args[0] == "clear")
        {
            if (args.Length == 1)
            {
                Console.Clear();
                return;
            }
            else
                throw new("~Nya! T-too many a-args!!~");
        }

        // Country
        if (args[0] == "country")
        {
            // TODO
        }
        
        // Unknown command exception~~
        throw new("~Nya! U-unknown command!!~\n" +
                  "T-try entering 'h'..!~");
    }

    private static void RunHelp()
    {
        Console.WriteLine("'h' or 'help' to list all commands (this text)\n" +
                          "'q' or 'quit' to quit the program\n" +
                          "'clear' to clear all text");
    }
}