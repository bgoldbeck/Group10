using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthcareClientSystem
{
    class ProviderTerminal : OperatorTerminal
    {

        public ProviderTerminal()
        {
            updateDelegates[(int)TerminalState.VIEW_PROVIDER_DIRECTORY] = ViewProviderDirectoryUpdate;
        }


        protected bool ViewProviderDirectoryUpdate()
        {

            string[] s = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m" };
            int groupSize = 5;
            tui.WriteList(s, groupSize);

            currentState = TerminalState.MENU;
            return true;
        }

        protected override bool MenuUpdate()
        {
            if (tui == null)
            {
                // Exception.
            }
            tui.WriteLine("Provider Terminal [Menu]", TextUI.TextUIJustify.CENTER);
            //tui.WriteLine("        " + (frame++).ToString());
            tui.WriteLine("");
            tui.WriteLine("");
            tui.WriteLine("");
            tui.WriteLine("    MENU OPTIONS ");
            tui.WriteLine("\t1) View Provider Directory");
           
            
          

            tui.WriteLine(" ");
            tui.WriteLine("\tl) Logout");
            tui.WriteLine("\t0) Exit Program");

            tui.Render();

            string userInput = Console.ReadLine();

            // Depending on user input, we change the state to match.

            switch (userInput)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    currentState = TerminalState.VIEW_PROVIDER_DIRECTORY;
                    break;
                case "4":
                    break;
                default:
                    break;
            }

            tui.WriteLine(userInput, TextUI.TextUIJustify.CENTER);
            return (userInput.Equals("0") == true) ? false : true;
        }
    }

}