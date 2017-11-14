using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ChocAnServer;
using ChocAnServer.Packets;
using HealthcareClientSystem.IO;

namespace HealthcareClientSystem
{
    public class ProviderTerminal : OperatorTerminal
    {

        public ProviderTerminal() : base()
        {
            updateDelegates[(int)TerminalState.VIEW_PROVIDER_DIRECTORY] = ViewProviderDirectoryUpdate;
            updateDelegates[(int)TerminalState.CHECK_MEMBER_STATUS] = CheckMemberStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool CheckMemberStatus()
        {
            currentState = TerminalState.MENU;
            return true;
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool ViewProviderDirectoryUpdate()
        {
            tui.WriteLine("ADD INVOICE", TextUI.TextUIJustify.CENTER);

            ResponsePacket responsePacket = server.ProcessAction(new BasePacket("VIEW_PROVIDER_DIRECTORY", sessionID));

            // View the response from the server.
            WriteResponse(responsePacket);
            
            // Pause for the user to look at the response.
            Console.ReadLine();

            // Now view the provider directory file.
            string [] contents = System.IO.File.ReadAllLines("ProviderDirectory.txt");
            
            
            // View the contents of the provider directory with the given group size paramter.
            tui.WriteList(contents, 15);

            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
           
            
          

            tui.WriteLine("\t2) Check Member Status");
            tui.WriteLine("\t3) Create Service Record");
            
            tui.WriteLine("\t9) Logout");
            tui.WriteLine("\t0) Exit Program");

            tui.Render();

            string userInput = Console.ReadLine();

            // Depending on user input, we change the state to match.

            switch (userInput)
            {
                case "1":
                    currentState = TerminalState.VIEW_PROVIDER_DIRECTORY;
                    break;
                case "2":
                    currentState = TerminalState.CHECK_MEMBER_STATUS;
                    break;
                case "3":
                    currentState = TerminalState.ADD_SERVICE_RECORD;
                    break;
                case "9":
                    currentState = TerminalState.LOGIN;
                    sessionID = "";
                    break;
                default:
                    break;
            }
            
            return (userInput.Equals("0") == true) ? false : true;
        }

        protected override int AccessLevel()
        {
            return 0;
        }
    }

}