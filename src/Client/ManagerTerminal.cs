using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChocAnServer;
using ChocAnServer.Packets;

namespace HealthcareClientSystem
{
    public class ManagerTerminal : OperatorTerminal
    {
        

        public ManagerTerminal() : base()
        {
            updateDelegates[(int)TerminalState.ADD_MEMBER] = AddMemberUpdate;
            updateDelegates[(int)TerminalState.ADD_PROVIDER] = AddProviderUpdate;
            updateDelegates[(int)TerminalState.ADD_SERVICE_CODE] = AddServiceCodeUpdate;
            updateDelegates[(int)TerminalState.ADD_SERVICE_RECORD] = AddServiceRecordUpdate;
            updateDelegates[(int)TerminalState.REMOVE_MEMBER] = RemoveMemberUpdate;
            updateDelegates[(int)TerminalState.REMOVE_PROVIDER] = RemoveProviderUpdate;
            updateDelegates[(int)TerminalState.REMOVE_SERVICE_RECORD] = RemoveServiceRecordUpdate;
            updateDelegates[(int)TerminalState.CUSTOM_MEMBER_REPORT] = CustomMemberReportUpdate;
            updateDelegates[(int)TerminalState.CUSTOM_PROVIDER_REPORT] = CustomProviderReportUpdate;
            updateDelegates[(int)TerminalState.UPDATE_MEMBER] = UpdateMemberUpdate;
            updateDelegates[(int)TerminalState.UPDATE_PROVIDER] = UpdateProviderUpdate;
            updateDelegates[(int)TerminalState.UPDATE_SERVICE_CODE] = UpdateServiceCodeUpdate;
            updateDelegates[(int)TerminalState.MAIN_ACCOUNTING_PROCEDURE] = MainAccountingProcedureUpdate;

        }

        /// <summary>
        /// The manager entered the add member state and now must fill out a
        /// member packet to submit to the server. The packet will process on the server
        /// and return a response that can be viewed and inspected to see what took place
        /// on the server-side.
        /// </summary>
        /// <returns></returns>
        private bool AddMemberUpdate()
        {
            tui.WriteLine("ADD MEMBER", TextUI.TextUIJustify.CENTER);

            // Fill out the new member packet from the user input and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(
                packetFactory.ReadPacket(tui, "MemberPacket", "ADD_MEMBER", sessionID) as MemberPacket);

            // Write the response packet to the screen buffer.
            tui.WriteLine("\n \n \t[Response]");
            tui.WriteLine(responsePacket.ToString());
            tui.WriteLine("\n \nType some key(s) to continue.", TextUI.TextUIJustify.CENTER);

            // Refresh the screen to see what our response was.
            tui.Refresh();

            // Pause for the user to look at the response.
            Console.ReadLine();

            // Just go straight back to menu. We are done.
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool AddProviderUpdate()
        {
            tui.WriteLine("ADD PROVIDER", TextUI.TextUIJustify.CENTER);

            // Fill out the new member packet from the user input and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(
                packetFactory.ReadPacket(tui, "ProviderPacket", "ADD_PROVIDER", sessionID) as ProviderPacket);

            // Write the response packet to the screen buffer.
            tui.WriteLine("\n \n \t[Response]");
            tui.WriteLine(responsePacket.ToString());
            tui.WriteLine("\n \nType some key(s) to continue.", TextUI.TextUIJustify.CENTER);

            // Refresh the screen to see what our response was.
            tui.Refresh();

            // Pause for the user to look at the response.
            Console.ReadLine();

            // Just go straight back to menu. We are done.
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool AddServiceCodeUpdate()
        {
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool AddServiceRecordUpdate()
        {
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool RemoveMemberUpdate()
        {
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool RemoveProviderUpdate()
        {
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool RemoveServiceRecordUpdate()
        {
            currentState = TerminalState.MENU;
            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CustomMemberReportUpdate()
        {
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CustomProviderReportUpdate()
        {
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool UpdateMemberUpdate()
        {
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool UpdateProviderUpdate()
        {
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool UpdateServiceCodeUpdate()
        {
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool MainAccountingProcedureUpdate()
        {
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
            tui.WriteLine("Manager Terminal [Menu]", TextUI.TextUIJustify.CENTER);
            //tui.WriteLine("        " + (frame++).ToString());
            tui.WriteLine("");
            tui.WriteLine("");
            tui.WriteLine("");
            tui.WriteLine("    MENU OPTIONS ");
            tui.WriteLine("\ta) Add Member");
            tui.WriteLine("\td) Deactivate Member");
            tui.WriteLine("\ts) Update Member");

            tui.WriteLine(" ");

            tui.WriteLine("\tu) Add Provider");
            tui.WriteLine("\ti) Deactivate Provider");
            tui.WriteLine("\to) Update Provider");

            tui.WriteLine(" ");

            tui.WriteLine("\tq) Add Service Code");
            tui.WriteLine("\tw) Update Service Code");
            tui.WriteLine("\te) Remove Service Code");

            tui.WriteLine(" ");

            tui.WriteLine("\tz) Custom Member Report");
            tui.WriteLine("\tx) Custom Provider Report");

            tui.WriteLine(" ");

            tui.WriteLine("\tf) Add Invoice");

            tui.WriteLine(" ");

            tui.WriteLine("\tm) Execute Main Accounting Procedure");

            tui.WriteLine(" ");
            tui.WriteLine("\t9) Logout");
            tui.WriteLine("\t0) Exit Program");

            tui.Render();

            string userInput = Console.ReadLine();

            // Depending on user input, we change the state to match.
            switch (userInput)
            {
                case "a":
                    currentState = TerminalState.ADD_MEMBER;
                    break;
                case "d":
                    currentState = TerminalState.REMOVE_MEMBER;
                    break;
                case "s":
                    currentState = TerminalState.UPDATE_MEMBER;
                    break;
                case "u":
                    currentState = TerminalState.ADD_PROVIDER;
                    break;
                case "i":
                    currentState = TerminalState.REMOVE_PROVIDER;
                    break;
                case "o":
                    currentState = TerminalState.UPDATE_PROVIDER;
                    break;
                case "q":
                    currentState = TerminalState.ADD_SERVICE_CODE;
                    break;
                case "w":
                    currentState = TerminalState.UPDATE_SERVICE_CODE;
                    break;
                case "e":
                    currentState = TerminalState.REMOVE_SERVICE_CODE;
                    break;
                case "z":
                    currentState = TerminalState.CUSTOM_MEMBER_REPORT;
                    break;
                case "x":
                    currentState = TerminalState.CUSTOM_PROVIDER_REPORT;
                    break;
                case "m":
                    currentState = TerminalState.MAIN_ACCOUNTING_PROCEDURE;
                    break;
                case "9":
                    currentState = TerminalState.LOGIN;
                    sessionID = "";
                    break;
                default:
                    break;
            }

            tui.WriteLine(userInput, TextUI.TextUIJustify.CENTER);
            return (userInput.Equals("0") == true) ? false : true;
        }

        protected override int AccessLevel()
        {
            return 1;
        }
    }

}