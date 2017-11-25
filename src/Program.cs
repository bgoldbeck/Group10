

using System;

using HealthcareClientSystem;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Program
{

    static void Main()
    {
        // Mmmmm. This new background color is nice.
        Console.BackgroundColor = ConsoleColor.Blue;
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Select the terminal you would like to use:" +
                "\n1. Provider Terminal" +
                "\n2. Manager Terminal" +
                "\n0. Quit");

            string input = Console.ReadLine();

            OperatorTerminal terminal = null;

            if (input.Equals("1"))
            {
                terminal = new ProviderTerminal();
            }
            else if (input.Equals("2"))
            {
                terminal = new ManagerTerminal();
            }
            else if (input.Equals("0"))
            {
                running = false;
            }
          
            // Loop the program until the user wants to quit.E
            // Or maybe something goes wrong, like really, really wrong.
            if (terminal != null && running == true)
            {
                if (!terminal.Loop())
                    terminal = null;
            }
        }

        // Force a close on the database, this will hopefully make Daniel happy.
        // Here is an owl.
        //
        //         ^ ^
        //        (0,0)
        //        (  ()
        //        _| _|
        // Or maybe it's a cat?
        // Anyway, let's close it, enough screwing around.
        SQLLiteDatabaseCenter.DatabaseCenter.Singelton.Close();
        return;
    }
}