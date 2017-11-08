

using System;

using HealthcareClientSystem;


public class Program
{

    static void Main()
    {

        SQLLiteDatabaseCenter.DatabaseCenter.Singelton.Initialize();

        Console.ReadLine();

        Console.BackgroundColor = ConsoleColor.Blue;
        OperatorTerminal terminal = new ManagerTerminal();

        terminal.Loop();

        return;
    }
}