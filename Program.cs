using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        
           
        OperatorTerminal terminal = new ProviderTerminal();
          
           
        terminal.Loop();
        
            
         

        return;
    }
}

