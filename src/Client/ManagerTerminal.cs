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
    public class ManagerTerminal : OperatorTerminal
    {
        
        /// <summary>
        /// Constructor for derived ManagerTerminal Class -> calls base class constructor
        /// </summary>
        public ManagerTerminal() : base()
        {
            updateDelegates[(int)TerminalState.ADD_MEMBER] = AddMemberUpdate;
            updateDelegates[(int)TerminalState.ADD_PROVIDER] = AddProviderUpdate;
            updateDelegates[(int)TerminalState.ADD_SERVICE_CODE] = AddServiceCodeUpdate;
            updateDelegates[(int)TerminalState.REMOVE_MEMBER] = RemoveMemberUpdate;
            updateDelegates[(int)TerminalState.REMOVE_PROVIDER] = RemoveProviderUpdate;
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
        /// <returns>True unless exceptions are thrown</returns>
        private bool AddMemberUpdate()
        {
            // Fill out the new member packet from the user input and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(
                packetFactory.BuildPacket(tui, "MemberPacket", currentState.ToString(), sessionID) as MemberPacket);

            // Write the response packet to the terminal
            WriteResponse(responsePacket);

            // Just go straight back to menu. We are done.
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// The manager wants to make a request to add a new provider to the database.
        /// The manager must fill out a provider packet with all the provider's information
        /// to be sent to the server to handle the processing. We will get a response that
        /// informs us of success/failure.
        /// </summary>
        /// <returns>True unless exceptions are thrown</returns>
        private bool AddProviderUpdate()
        {
            // Fill out the new member packet from the user input and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(
                packetFactory.BuildPacket(tui, "ProviderPacket", currentState.ToString(), sessionID) as ProviderPacket);

            // Write the response packet to the terminal
            WriteResponse(responsePacket);

            // Just go straight back to menu. We are done.
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// The manager entered the add service code state and now must fill out a
        /// service code packet to submit to the server. The service code represents
        /// services that providers offer their customers. The packet will process on the server
        /// and return a response that can be viewed and inspected to see what took place
        /// on the server-side.
        /// </summary>
        /// <returns>True, unless exceptions are thrown</returns>
        private bool AddServiceCodeUpdate()
        {
            // Fill out the new service code packet from the user input and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(
                packetFactory.BuildPacket(tui, "ServiceCodePacket", "ADD_SERVICE_CODE", sessionID) as ServiceCodePacket);

            // View the response packet and pause until user enters a key.
            WriteResponse(responsePacket);


            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// The manager wants to make a request to remove a member from the database.
        /// The manager must fill out a member id they wish to deactivate.
        /// This will be sent to the server to handle the processing. We will get a response that
        /// informs us of success/failure.
        /// </summary>
        /// <returns>True, unless exceptions are thrown</returns>
        private bool RemoveMemberUpdate()
        {
            // Fill out the new member packet from the user input and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(new MemberPacket("REMOVE_MEMBER", sessionID, 
                InputController.ReadInteger(9, 9, true, "Member ID").ToString(), "", "", "", "", "", "", ""));

            // View the response packet and pause until user enters a key.
            WriteResponse(responsePacket);

            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// The manager wants to make a request to remove a provider from the database.
        /// The manager must fill out a provider id they wish to deactivate.
        /// This will be sent to the server to handle the processing. We will get a response that
        /// informs us of success/failure.
        /// </summary>
        /// <returns>True, unless exceptions are thrown</returns>
        private bool RemoveProviderUpdate()
        {
            // Fill out the new provider packet from the user input and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(new ProviderPacket("REMOVE_PROVIDER", sessionID,
                InputController.ReadInteger(9, 9, true, "Provider ID").ToString(), "", "", "", "", "", "", "", ""));

            // View the response packet and pause until user enters a key.
            WriteResponse(responsePacket);

            currentState = TerminalState.MENU;
            return true;
        }
        
        /// <summary>
        /// The manager wants to view a specific member's report. It will contain
        /// a list of invoices the member has been serviced by within a certain range.
        /// The user must fill in this information to be sent to the server. The server will process
        /// the information and return a response with success/failure.
        /// </summary>
        /// <returns>True, unless exceptions are thrown</returns>
        private bool CustomMemberReportUpdate()
        {
            // Fill out a custom member report packet and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(
                packetFactory.BuildPacket(tui, "DateRangePacket", "CUSTOM_MEMBER_REPORT", sessionID) as DateRangePacket);

            // View the response packet and pause until user enters a key.
            WriteResponse(responsePacket);

            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// The manager wants to view a specific providers's report. It will contain
        /// a list of invoices the provider has serviced by within a certain range.
        /// The user must fill in this information to be sent to the server. The server will process
        /// the information and return a response with success/failure.
        /// </summary>
        /// <returns>True, unless exceptions are thrown</returns>
        private bool CustomProviderReportUpdate()
        {
            // Fill out a custom member report packet and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(
                packetFactory.BuildPacket(tui, "DateRangePacket", "CUSTOM_PROVIDER_REPORT", sessionID) as DateRangePacket);

            // View the response packet and pause until user enters a key.
            WriteResponse(responsePacket);
            
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// The manager entered the update member state and now must fill out a
        /// member packet to submit to the server for updating a member's information. 
        /// The packet will process on the server and return a response that can be viewed 
        /// and inspected to see what took place on the server-side.
        /// </summary>
        /// <returns>True, unless exceptions are thrown</returns>
        private bool UpdateMemberUpdate()
        {
            // Fill out the new member packet from the user input and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(
                packetFactory.BuildPacket(tui, "MemberPacket", "UPDATE_MEMBER", sessionID) as MemberPacket);

            // Write the response packet to the terminal
            WriteResponse(responsePacket);

            // Just go straight back to menu. We are done.
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// The manager entered the update provider state and now must fill out a
        /// provider packet to submit to the server for updating a provider's information. 
        /// The packet will process on the server and return a response that can be viewed 
        /// and inspected to see what took place on the server-side.
        /// </summary>
        /// <returns>True, unless exceptions are thrown</returns>
        private bool UpdateProviderUpdate()
        {
            // Fill out the new provider packet from the user input and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(
                packetFactory.BuildPacket(tui, "ProviderPacket", currentState.ToString(), sessionID) as ProviderPacket);

            // Write the response packet to the terminal
            WriteResponse(responsePacket);

            // Just go straight back to menu. We are done.
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// The manager entered the update service code state and now must fill out a
        /// service code packet to submit to the server for updating a service codes's information. 
        /// The packet will process on the server and return a response that can be viewed 
        /// and inspected to see what took place on the server-side.
        /// </summary>
        /// <returns>True, unless exceptiosn are thrown</returns>
        private bool UpdateServiceCodeUpdate()
        {
            // Fill out the new service code packet from the user input and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(
                packetFactory.BuildPacket(tui, "ServiceCodePacket", "UPDATE_SERVICE_CODE", sessionID) as ServiceCodePacket);

            // View the response packet and pause until user enters a key.
            WriteResponse(responsePacket);
            
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// The manager requests the Main Accounting Procedure. All the reports requested
        /// will be generated by the server when the request reaches to the server. The server
        /// will send a response when all the reports have been generated. The response will inform
        /// the user how many reports were generated or of failure status.
        /// </summary>
        /// <returns>True, unless exceptions are thrown</returns>
        private bool MainAccountingProcedureUpdate()
        {
            ResponsePacket responsePacket = server.ProcessAction(
                new BasePacket("MAIN_ACCOUNTING_PROCEDURE", sessionID));

            // View the response packet and pause until user enters a key.
            WriteResponse(responsePacket);

            // Return to the main menu.
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// The main menu. Display all the choices to the user on the terminal screen.
        /// </summary>
        /// <returns>Returns user input. If false, program exits</returns>
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
                case "f":
                    currentState = TerminalState.ADD_SERVICE_RECORD;
                    break;
                case "m":
                    currentState = TerminalState.MAIN_ACCOUNTING_PROCEDURE;
                    break;
                case "9":
                    currentState = TerminalState.LOGIN;
                    sessionID = userID = "";
                    break;
                default:
                    break;
            }

            tui.WriteLine(userInput, TextUI.TextUIJustify.CENTER);
            return (userInput.Equals("0") == true) ? false : true;
        }

        ///<summary>
        /// ????????????
        ///</summary>
        ///<returns>1</returns>
        protected override int AccessLevel()
        {
            return 1;
        }

        /// <summary>
        /// Needed to unit test this. 
        /// AccessLevel() may be unneeded. 
        /// </summary>
        public int GetAccessLevel()
        {
            return AccessLevel();
        }
    }

}