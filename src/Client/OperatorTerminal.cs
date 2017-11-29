/*This class is will be the virtual base type for the derived terminal object types.
 *It manages shared functionality, such as looping the program, adding an invoice, and
 *login handling.
 */
using System;
using ChocAnServer.Packets;
using HealthcareClientSystem.IO;

namespace HealthcareClientSystem
{

    public class OperatorTerminal
    { 
        /// <summary>
        /// an enum of all the states the program can possibly be in.
        /// </summary>
        public enum TerminalState
        {
            LOGIN, MENU, VIEW_PROVIDER_DIRECTORY,
            ADD_MEMBER, CHECK_MEMBER_STATUS,
            UPDATE_MEMBER, REMOVE_MEMBER, ADD_PROVIDER,
            ADD_SERVICE_CODE, REMOVE_SERVICE_CODE, ADD_SERVICE_RECORD,
            MAIN_ACCOUNTING_PROCEDURE, REMOVE_PROVIDER,
            UPDATE_PROVIDER, UPDATE_SERVICE_CODE,
            CUSTOM_MEMBER_REPORT, CUSTOM_PROVIDER_REPORT, COUNT
        };

        /// <summary>
        /// Mock variables
        /// </summary>
        private static Boolean MockMode = false;
        private static TerminalState MockState = TerminalState.LOGIN;
        public static BasePacket MockPacket = null;

        /// <summary>
        /// Manages our screen space and organizes output to the console.
        /// </summary>
        protected TextUI tui;

       

        /// <summary>
        /// The specific state the program is currently in.
        /// </summary>
        protected TerminalState currentState;

        /// <summary>
        /// A function pointer that determines all other update functions.
        /// </summary>
        /// <returns></returns>
        protected delegate bool UpdateDelegate();

        /// <summary>
        /// An array of function pointers to update functions that depend on the program state.
        /// </summary>
        protected UpdateDelegate[] updateDelegates;

        /// <summary>
        /// The number of columns in the screen space.
        /// </summary>
        protected int columnSize;

        /// <summary>
        /// The number of rows in the screen space.
        /// </summary>
        protected int rowSize;

        /// <summary>
        /// The current session id for the user.
        /// </summary>
        protected string sessionID;

        /// <summary>
        /// An object of the server, normally this instance would be a separate program.
        /// </summary>
        protected ChocAnServer.ChocAnServer server;

        /// <summary>
        /// An object of the packetFactory for building packets.
        /// </summary>
        protected PacketFactory packetFactory;

        /// <summary>
        /// The user id of the current operator.
        /// </summary>
        protected string userID;

        protected Boolean isFake;

        /// <summary>
        /// Initialize all data members.
        /// </summary>
        public OperatorTerminal(Boolean isFake = false)
        {
            this.isFake = isFake;

            // Set up the default window size.
            columnSize = (int)((float)Console.LargestWindowWidth / 2f);
            rowSize = (int)((float)Console.LargestWindowHeight / 1.4f);
        
            if(!isFake)
            { 
                Console.SetWindowSize(columnSize + 1, rowSize + 2);
            }

            // Create the text ui instance.
            tui = new TextUI(rowSize, columnSize, isFake);
            
            // Default state is login.
            currentState = TerminalState.LOGIN;

            // Setup the array of updateDelegates.
            updateDelegates = new UpdateDelegate[(int)TerminalState.COUNT];

            // Set each updateDelegate.
            updateDelegates[(int)TerminalState.LOGIN] = LoginUpdate;
            updateDelegates[(int)TerminalState.MENU] = MenuUpdate;
            updateDelegates[(int)TerminalState.ADD_SERVICE_RECORD] = AddServiceRecordUpdate;

            // Initially, no user logged in.
            sessionID = "";
            userID = "";

            // Generate a server instance.
            server = new ChocAnServer.ChocAnServer();

            // Generate a packet factory instance.
            packetFactory = new PacketFactory();

        }

        /// <summary>
        /// The operator entered the add service record state and now must fill out an
        /// invoice packet to submit to the server. The packet will process on the server
        /// and return a response that can be viewed and inspected to see what took place
        /// on the server-side.
        /// </summary>
        /// <returns>boolean</returns>
        protected bool AddServiceRecordUpdate()
        {
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
        /// The main program loop, we loop until the user decides to exit the
        /// entire program or if an unrecoverable error would happen while the
        /// user is interacting with the program in various states.
        /// </summary>
        /// <returns>
        /// False, if the program should no longer continue running.
        /// </returns>
        public bool Loop()
        {
            // Initialize program to running.
            bool running = true;

            // Loop while the user has decided not to exit.
            while (running)
            {
                // Make sure we have a text ui instance
                if (tui != null)
                {
                    // Clear the screen buffer on each pass.
                    tui.ClearBuffer();

                    // Make sure our function pointer is not null before running the function.
                    if (updateDelegates[(int)currentState] != null)
                    {
                        // Run the function and return whether we should continue running the program.
                        running = updateDelegates[(int)currentState]();
                        
                        // Our header is based on our current state.
                        tui.Header = " | " + currentState.ToString().Replace('_', ' ') + " | ";

                        // Clear the footer (most recent message).
                        tui.Footer = "";
                    }
                    else
                    {
                        // We probably should run the program while we have null function pointers.
                        running = false;
                    }
                }
                else
                {
                    // We can't run the program without a text ui instance.
                    running = false;
                }

            }
            return running;
        }

        /// <summary>
        /// For testing the update delegates wirthout risking infinite loop of conventional means.
        /// ONLY USE FOR UNIT TESTS
        /// </summary>
        /// <param name="terminalState"></param>
        /// <returns>Whether or not the console will continue to run</returns>
        public bool _RunUpdateDelegateOnce(TerminalState terminalState)
        {
            currentState = terminalState;
            userID = "123456789";
            if (updateDelegates[(int)terminalState] != null)
            {
                return updateDelegates[(int)terminalState]();
            }
            return false;
        }

        /// <summary>
        /// Build a login packet from user input from the packet factory. Send
        /// the packet to the server for processing. The server will send back a 
        /// session id if login was successful.
        /// </summary>
        /// <returns>True</returns>
        private bool LoginUpdate()
        {
            if (tui == null)
            {
                // Exception.
            }
            LoginPacket loginPacket = null;

            // Build a login packet.
            if (MockMode == true)
            {
                loginPacket = MockPacket as LoginPacket;
            }
            else
            { 
                loginPacket = packetFactory.BuildPacket(
                    tui, "LoginPacket", "", "", "", AccessLevel()) as LoginPacket;
            }

            // Send the packet to the server and instantiate a response.
            ResponsePacket responsePacket = server.ProcessAction(loginPacket);

            if (MockMode == true)
            {
                MockPacket = responsePacket as ResponsePacket;
            }

            // Get the session id from the response packet.
            this.sessionID = responsePacket.Data();
            
            // If we got a valid session from the server (md5 is length 32)
            if (this.sessionID.Length == 32)
            { 
                // Login! Success!
                userID = loginPacket.ID();

                // Change state to menu.
                currentState = TerminalState.MENU;
            }
            else
            { 
                // Something went wrong with login information, show the operator the response.
                WriteResponse(responsePacket);
            }
            return MockMode == true ? false : true;
        }


        /// <summary>
        /// Virtual function with no real logic, meant to be overridden in 
        /// derived classes.
        /// </summary>
        /// <returns>True</returns>
        protected virtual bool MenuUpdate()
        {
            return true;
        }

        /// <summary>
        /// Operator is not a real terminal type, return -1 for an
        /// incorrect access level.
        /// </summary>
        /// <returns>
        /// -1
        /// </returns>
        public virtual int AccessLevel()
        {
            return -1;
        }

        /// <summary>
        /// Returns true if the session id field is not empty.
        /// </summary>
        /// <returns>
        /// True if the user is logged in, false otherwise.
        /// </returns>
        public bool IsLoggedIn()
        {
            return sessionID != "";
        }

        /// <summary>
        /// Write the packet response to the screen space. Including the session id,
        /// action performed, any packet data, and the response text from the server.
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
            tui.WriteLine("\n \nPress any to continue.", TextUI.TextUIJustify.CENTER);

            // Set the footer message.
            tui.Footer = " " + packet.Response() + " ";

            // Refresh the screen to see what our response was.
            tui.Refresh();

            //Wait for any single key to be pressed by the user.
            if (!isFake)
                Console.ReadKey();

            return;
        }

        /// <summary>
        /// Places the operator Terminal in mock mode
        /// This does not take any input from the user
        /// </summary>
        public static void EnableMock()
        {
            MockMode = true;
            MockState = TerminalState.LOGIN;
            MockPacket = null;
            return;
        }
        /// <summary>
        /// Places the operator Terminal in mock mode
        /// This does not take any input from the user
        /// </summary>
        public static void DisableMock()
        {
            MockMode = false;
            MockState = TerminalState.LOGIN;
            MockPacket = null;
            return;
        }

        /// <summary>
        /// Adds state to be mocked if the program needs to be in a state of any kind
        /// The state is cleared whenever Mock is enabled/disabled
        /// </summary>
        /// <param name="state">State to set for test.</param>
        public static void ChangeMockState(TerminalState state)
        {
            MockState = state;
            return;
        }

        /// <summary>
        /// Adds packet to be mocked if the program needs a packet of any kind
        /// The packet is cleared whenever Mock is enabled/disabled
        /// </summary>
        /// <param name="packet">State to set for test.</param>
        public static void ChangeMockPacket(BasePacket packet)
        {
            MockPacket = packet;
            return;
        }
    }
}
