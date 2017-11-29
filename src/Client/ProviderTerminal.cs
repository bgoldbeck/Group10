/*The ProviderTerminal is a temporary provider terminal used for software validation till 
 * Comm’R’us finishes the actual terminal. This job of the terminal is to allow a 
 * provider to login and access the features that they are expecting to use. The
 * class is derived from the OperatorTerminal class. This class allows a provider
 * to access the login functionality and then lets them have access to all the 
 * functionality that a provider needs.
 * */
using System;
using ChocAnServer.Packets;
using HealthcareClientSystem.IO;

namespace HealthcareClientSystem
{
    public class ProviderTerminal : OperatorTerminal
    {
        /// <summary>
        /// Constructor for the ProviderTerminalClass -> calls base class constructor
        /// </summary>
        public ProviderTerminal(Boolean isFaked = false) : base(isFaked)
        {
            updateDelegates[(int)TerminalState.VIEW_PROVIDER_DIRECTORY] = ViewProviderDirectoryUpdate;
            updateDelegates[(int)TerminalState.CHECK_MEMBER_STATUS] = CheckMemberStatus;
        }

        /// <summary>
        /// Executes a call to server for member status
        /// </summary>
        /// <returns>True, unless exceptions are thrown</returns>
        protected bool CheckMemberStatus()
        {
            tui.WriteLine("CHECK MEMBER STATUS", TextUI.TextUIJustify.CENTER);
            
            ResponsePacket responsePacket = server.ProcessAction(new MemberPacket("MEMBER_STATUS", sessionID,
                InputController.ReadInteger(9, 9, true, "MemberID").ToString(), "", "", "", "", "", "", ""));

            // Write the response packet to the terminal. 
            // Will pause until the user presses a key.
            WriteResponse(responsePacket);
            

            // Just go straight back to menu. We are done.
            currentState = TerminalState.MENU;
            return true;
        }
        

        /// <summary>
        /// Calls to server to get provider directory to view in textUI
        /// </summary>
        /// <returns>True, unless exceptions are thrown</returns>
        protected bool ViewProviderDirectoryUpdate()
        {
            tui.WriteLine("ADD INVOICE", TextUI.TextUIJustify.CENTER);

            ResponsePacket responsePacket = server.ProcessAction(new BasePacket("VIEW_PROVIDER_DIRECTORY", sessionID));

            // View the response from the server.
            WriteResponse(responsePacket);

            // Now view the provider directory file.
            string [] contents = System.IO.File.ReadAllLines("ProviderDirectory.txt");
            
            
            // View the contents of the provider directory with the given group size paramter.
            tui.WriteList(contents, 15);

            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// Updates menu
        /// </summary>
        /// <returns>true, unless exceptions are thrown</returns>
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
            tui.WriteLine("\t0) Exit Terminal");

            tui.Render();

            string userInput = InputController.ReadText(0, 100);

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

        public override int AccessLevel()
        {
            return 0;
        }
    }

}