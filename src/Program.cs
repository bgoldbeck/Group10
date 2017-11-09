

using System;

using HealthcareClientSystem;


public class Program
{

    static void Main()
    {

        // Mmmmm. This new background color is nice.
        Console.BackgroundColor = ConsoleColor.Blue;

        // We need to incorporate the kind of terminal the user wants.
        OperatorTerminal terminal = new ManagerTerminal();

        // Loop the program until the user wants to quit.
        // Or maybe something goes wrong, like really, really wrong.
        terminal.Loop();

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