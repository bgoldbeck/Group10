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
    public class OperatorTerminal
    {
        protected TextUI tui;

        protected enum TerminalState {
            LOGIN, MENU, VIEW_PROVIDER_DIRECTORY,
            ADD_MEMBER, CHECK_MEMBER_STATUS,
            UPDATE_MEMBER, REMOVE_MEMBER, ADD_PROVIDER, 
            ADD_SERVICE_CODE, REMOVE_SERVICE_CODE, ADD_SERVICE_RECORD,
            MAIN_ACCOUNTING_PROCEDURE, REMOVE_PROVIDER,
            UPDATE_PROVIDER, UPDATE_SERVICE_CODE,
            CUSTOM_MEMBER_REPORT, CUSTOM_PROVIDER_REPORT, COUNT };

        protected TerminalState currentState;

        protected delegate bool UpdateDelegate();

        protected UpdateDelegate[] updateDelegates;

        protected int columnSize;
        protected int rowSize;

        protected string sessionID;

        protected ChocAnServer.ChocAnServer server;

        protected PacketFactory packetFactory;

        protected string userID;

        /// <summary>
        /// 
        /// </summary>
        public OperatorTerminal()
        {
            columnSize = 100;
            rowSize = 40;

            Console.SetWindowSize(columnSize + 1, rowSize + 2);

            tui = new TextUI(rowSize, columnSize);
            
            currentState = TerminalState.LOGIN;

            // Setup the array of updateDelegates.
            updateDelegates = new UpdateDelegate[(int)TerminalState.COUNT];

            // Set each updateDelegate.
            updateDelegates[(int)TerminalState.LOGIN] = LoginUpdate;
            updateDelegates[(int)TerminalState.MENU] = MenuUpdate;
            updateDelegates[(int)TerminalState.ADD_SERVICE_RECORD] = AddServiceRecordUpdate;

            sessionID = "";

            server = new ChocAnServer.ChocAnServer();

            packetFactory = new PacketFactory();

            userID = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool AddServiceRecordUpdate()
        {
            tui.WriteLine("ADD INVOICE", TextUI.TextUIJustify.CENTER);

            // Fill out the new invoice packet from the user input and send it off to the server.
            ResponsePacket responsePacket = server.ProcessAction(
                packetFactory.BuildPacket(tui, "InvoicePacket", "ADD_INVOICE", sessionID, userID) as InvoicePacket);

            // Write the response packet to the terminal
            WriteResponse(responsePacket);

            // Just go straight back to menu. We are done.
            currentState = TerminalState.MENU;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Loop()
        {
            bool running = true;
            while (running)
            {
                if (tui != null)
                {
                    tui.ClearBuffer();
                    if (updateDelegates[(int)currentState] != null)
                    {
                        running = updateDelegates[(int)currentState]();
                        tui.Header = " | " + currentState.ToString() + " | ";
                        tui.Footer = "";
                    }
                }

            }
            return running;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool LoginUpdate()
        {
            if (tui == null)
            {
                // Exception.
            }

            LoginPacket loginPacket = packetFactory.BuildPacket(tui, "LoginPacket", "", "", "", AccessLevel()) as LoginPacket;

            ResponsePacket responsePacket = server.ProcessAction(loginPacket);

            this.sessionID = responsePacket.Data();


            // If we got a valid session from the server (md5 is length 32)
            if (this.sessionID.Length == 32)
            { 
                userID = loginPacket.ID();
                currentState = TerminalState.MENU;
            }
            else
                WriteResponse(responsePacket);

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual bool MenuUpdate()
        {

            return true;
        }

        protected virtual int AccessLevel()
        {
            return -1;
        }

        public bool IsLoggedIn()
        {
            return sessionID != "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        protected void WriteResponse(ResponsePacket packet)
        {
            if (packet == null)
            {
                throw new ArgumentNullException("packet", "Response packet was null for reading.");
            }

            // Write the response packet to the screen buffer.
            tui.WriteLine("\n \n \t[Response]");
            tui.WriteLine(packet.ToString());
            tui.WriteLine("\n \nType some key(s) to continue.", TextUI.TextUIJustify.CENTER);

            tui.Footer = " " + packet.Response() + " ";

            // Refresh the screen to see what our response was.
            tui.Refresh();

            //Wait for any single key to be pressed by the user.
            Console.ReadKey();

            return;
        }
    }
}
