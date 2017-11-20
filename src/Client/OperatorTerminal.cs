// Copyright <2017> 

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
// and associated documentation files (the "Software"), to deal in the Software without 
// restriction, including without limitation the rights to use, copy, modify, merge, publish, 
// distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom 
// the Software is furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or 
// substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
    /// <summary>
    /// This class is will be the virtual base type for the derived terminal object types.
    /// It manages shared functionality, such as looping the program, adding an invoice, and
    /// login handling.
    /// </summary>
    public class OperatorTerminal
    {
        /// <summary>
        /// Manages our screen space and organizes output to the console.
        /// </summary>
        protected TextUI tui;

        /// <summary>
        /// an enum of all the states the program can possibly be in.
        /// </summary>
        protected enum TerminalState {
            LOGIN, MENU, VIEW_PROVIDER_DIRECTORY,
            ADD_MEMBER, CHECK_MEMBER_STATUS,
            UPDATE_MEMBER, REMOVE_MEMBER, ADD_PROVIDER, 
            ADD_SERVICE_CODE, REMOVE_SERVICE_CODE, ADD_SERVICE_RECORD,
            MAIN_ACCOUNTING_PROCEDURE, REMOVE_PROVIDER,
            UPDATE_PROVIDER, UPDATE_SERVICE_CODE,
            CUSTOM_MEMBER_REPORT, CUSTOM_PROVIDER_REPORT, COUNT };

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

        /// <summary>
        /// Initialize all data members.
        /// </summary>
        public OperatorTerminal()
        {
            // Set up the window size.
            columnSize = 100;
            rowSize = 40;
            Console.SetWindowSize(columnSize + 1, rowSize + 2);

            // Create the text ui instance.
            tui = new TextUI(rowSize, columnSize);
            
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

            // Build a login packet.
            LoginPacket loginPacket = packetFactory.BuildPacket(tui, "LoginPacket", "", "", "", AccessLevel()) as LoginPacket;

            // Send the packet to the server and instantiate a response.
            ResponsePacket responsePacket = server.ProcessAction(loginPacket);

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

            return true;
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
        protected virtual int AccessLevel()
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
            Console.ReadKey();

            return;
        }
    }
}
