using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChocAnServer;
using ChocAnServer.Packets;

namespace HealthcareClientSystem
{
    public class OperatorTerminal
    {
        protected TextUI tui;

        protected enum TerminalState {
            LOGIN, MENU, VIEW_PROVIDER_DIRECTORY,
            ADD_MEMBER, CHECK_MEMBER_STATUS, CREATE_SERVICE_RECORD,
            UPDATE_MEMBER, REMOVE_MEMBER, ADD_PROVIDER, 
            ADD_SERVICE_CODE, REMOVE_SERVICE_CODE, ADD_SERVICE_RECORD,
            REMOVE_SERVICE_RECORD, MAIN_ACCOUNTING_PROCEDURE, REMOVE_PROVIDER,
            UPDATE_PROVIDER, UPDATE_SERVICE_CODE,
            CUSTOM_MEMBER_REPORT, CUSTOM_PROVIDER_REPORT, COUNT };

        protected TerminalState currentState;

        protected delegate bool UpdateDelegate();

        protected UpdateDelegate[] updateDelegates;

        protected int columnSize;
        protected int rowSize;

        protected string sessionID;

        protected ChocAnServer.ChocAnServer server;

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

            sessionID = "";

            server = new ChocAnServer.ChocAnServer();

        }

        /// <summary>
        /// 
        /// </summary>
        public bool Loop()
        {
            //string userInput = Console.ReadLine();
            bool running = true;
            while (running)
            {
                if (tui != null)
                {
                    tui.ClearBuffer();
                    if (updateDelegates[(int)currentState] != null)
                    {
                        running = updateDelegates[(int)currentState]();
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
            tui.WriteLine("Terminal [Login]", TextUI.TextUIJustify.CENTER);
            //tui.WriteLine("        " + (frame++).ToString());
            tui.WriteLine("");
            tui.WriteLine("");
            tui.WriteLine("");
            tui.WriteLine("    Login ");

            tui.WriteLine("");
            tui.WriteLine("");
            tui.WriteLine("Enter your userID:\n", TextUI.TextUIJustify.CENTER);

            tui.Render(true);

            string username = Console.ReadLine();
            //string formattedoutput = String.Format("{0} {1}", username, username.Length);
            tui.WriteLine(String.Format("UserID: {0}\nEnter your password:\n", username), TextUI.TextUIJustify.CENTER);
            tui.Render(true);

            string password = Console.ReadLine();

            tui.WriteLine(String.Format("UserID: {0}\nPassword: {1}\n", username, password), TextUI.TextUIJustify.CENTER);
            tui.Render(true);

            //Do login packet here.
            LoginPacket lp = new LoginPacket("LOGIN", "", username, password, AccessLevel());

            ResponsePacket rp = server.ProcessAction(lp);

            this.sessionID = rp.Data();


            // If we got a valid session from the server (md5 is length 32)
            if (this.sessionID.Length == 32)
                currentState = TerminalState.MENU;
            else
            {
                tui.WriteLine(String.Format("The server gave the response:\n Status: {0} SessionID: {1}\nPress any key to continue...", rp.Response(), rp.Data()), TextUI.TextUIJustify.CENTER);
                tui.Render(true);
                Console.ReadKey();
            }

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
    }
}
