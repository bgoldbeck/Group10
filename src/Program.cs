

using System;

using HealthcareClientSystem;


public class Program
{

    static void Main()
    {

        
        Console.BackgroundColor = ConsoleColor.Blue;
        OperatorTerminal terminal = new ManagerTerminal();

        terminal.Loop();

        return;
    }
}