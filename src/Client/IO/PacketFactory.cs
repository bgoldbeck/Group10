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

using ChocAnServer.Packets;
using HealthcareClientSystem;

namespace HealthcareClientSystem.IO
{
    /// <summary>
    /// This class resembled the factory pattern and is responsible for generating packets 
    /// of various derived packet types. The packets objects are generated and the data
    /// in each is filled from user input. The only exposed public function from this class
    /// is the BuildPacket function, which is responible for determining which packet to build.
    /// </summary>
    public class PacketFactory
    {
        /// <summary>
        /// This function will build a packet of a type specified in the packetType
        /// parameter. The user will be required to fill in the necessary information
        /// to fullfill the packet parameters.
        /// </summary>
        /// <param name="tui">
        /// The text ui reference for outout to the screen space.
        /// </param>
        /// <param name="packetType">
        /// The type of packet to build.
        /// </param>
        /// <param name="action">
        /// The action the packet will perform.
        /// </param>
        /// <param name="sessionID">
        /// The sessionID of the current user.
        /// </param>
        /// <param name="accessLevel">
        /// The accessLevel for a login packetm, necessary when creating a
        /// login packet.
        /// </param>
        /// <returns>
        /// A new packet of the type requested from packetType or throws ArgumentException
        /// if not found.
        /// </returns>
        public BasePacket BuildPacket(TextUI tui, String packetType, 
            string action, string sessionID = "", string userID = "", int accessLevel = -1)
        {
            if (tui == null)
            {
                throw new ArgumentNullException("tui", "Expected TextUI");
            }

            BasePacket packet = null;
            
            // Find out what the packetType is of.
            switch (packetType)
            {
                case "MemberPacket":
                    packet = ReadMemberPacket(tui, action, sessionID);
                    break;
                case "ProviderPacket":
                    packet = ReadProviderPacket(tui, action, sessionID);
                    break;
                case "InvoicePacket":
                    packet = ReadInvoicePacket(tui, action, sessionID, userID);
                    break;
                case "LoginPacket":
                    packet = ReadLoginPacket(tui, accessLevel);
                    break;
                case "ServiceCodePacket":
                    packet = ReadServiceCodePacket(tui, action, sessionID);
                    break;
                case "DateRangePacket":
                    packet = ReadDateRangePacket(tui, action, sessionID);
                    break;
                default:
                    throw new ArgumentException("Invalid type", "packetType");
            }

            // The new packet has been created from user input, and now ready to leave the factory.
            return packet;
        }

        /// <summary>
        /// Attemps to read input from the user to fill out details in a member packet.
        /// </summary>
        /// <param name="tui">
        /// The text ui reference for outout to the screen space.
        /// </param>
        /// <param name="action">
        /// The action the packet will perform.
        /// </param>
        /// <param name="sessionID">
        /// The sessionID of the current user.
        /// </param>
        /// <returns>
        /// A newly created member packet from user input.
        /// </returns>
        private MemberPacket ReadMemberPacket(TextUI tui, string action, string sessionID)
        {
            // Nice message at the top.
            tui.WriteLine("\tPlease enter the member's details. \n ");
            tui.Render();

            // Default memberID to something we don't care about.
            string memberID = "000000001";
            
            // Default member status to active.
            string memberStatus = "ACTIVE";

            if (action == "UPDATE_MEMBER")
            {
                // Get the 9 digit, positive memberID from the user.
                memberID = InputController.ReadInteger(9, 9, true, "Member ID").ToString();

                tui.WriteLine("\tMemberID: " + memberID);
                tui.Refresh();

                // Get the new status of the member from the user.
                string memberStatusResponse = InputController.ReadText(
                    1, 1, "Member Active? [Y/y] YES [N/n] NO");

                if (memberStatusResponse != "Y" && memberStatusResponse != "y")
                {
                    // User chose member to be suspended.
                    memberStatus = "SUSPENDED";
                }
            }

            // Show member active status.
            tui.WriteLine("\tMemberActive: " + memberStatus);
            tui.Refresh();

            // Get the member's name.
            string memberName = InputController.ReadText(0, 25, "Member Name");

            // Show the name.
            tui.WriteLine("\tMemberName: " + memberName);
            tui.Refresh();

            // Get the member's address.
            string memberAddress = InputController.ReadText(0, 25, "Member Address");

            // Show the address.
            tui.WriteLine("\tMemberAddress: " + memberAddress);
            tui.Refresh();

            // Get the member's city.
            string memberCity = InputController.ReadText(0, 14, "Member City");

            // Show the city.
            tui.WriteLine("\tMemberCity: " + memberCity);
            tui.Refresh();

            // Get the member's state.
            string memberState = InputController.ReadText(0, 2, "Member State");

            // Show the state.
            tui.WriteLine("\tMemberState: " + memberState);
            tui.Refresh();

            // Get the member's zip code.
            string memberZip = InputController.ReadInteger(0, 5, true, "Member Zip").ToString();

            // Show the zip.
            tui.WriteLine("\tMemberZip: " + memberZip);
            tui.Refresh();

            // Get the member's zip email address.
            string memberEmail = InputController.ReadText(0, 254, "Member Email");
            tui.WriteLine("\tMemberEmail: " + memberEmail);

            // Create and return the new member packet.
            return new MemberPacket(action, sessionID, memberID, memberStatus, memberName, 
                memberAddress, memberCity, memberState, memberZip, memberEmail);
        }

        /// <summary>
        /// Attemps to read input from the user to fill out details in a provider packet.
        /// </summary>
        /// <param name="tui">
        /// The text ui reference for outout to the screen space.
        /// </param>
        /// <param name="action">
        /// The action the packet will perform.
        /// </param>
        /// <param name="sessionID">
        /// The sessionID of the current user.
        /// </param>
        /// <returns>
        /// A newly created provider packet from user input.
        /// </returns>
        private ProviderPacket ReadProviderPacket(TextUI tui, string action, string sessionID)
        {
            // Nice message at the top.
            tui.WriteLine("\tPlease enter the provider's details. \n ");
            tui.Render();

            // Some default providerID we dont care about.
            string providerID = "000000001";

            // Default provider is active.
            string providerStatus = "ACTIVE";


            if (action == "UPDATE_PROVIDER")
            {
                // Get the 9 digit, positive provider id.
                providerID = InputController.ReadInteger(9, 9, true, "Provider ID").ToString();

                // Show the provider id.
                tui.WriteLine("\tProviderID: " + providerID);
                tui.Refresh();

                // Determine if the provider is active.
                string providerStatusResponse = InputController.ReadText(
                    1, 1, "Provider Active? [Y/y] YES [N/n] NO");

                if (providerStatusResponse != "Y" && providerStatusResponse != "y")
                {
                    // User chose provider to be suspended.
                    providerStatus = "SUSPENDED";
                }
            }
        
            // Show the provider status.
            tui.WriteLine("\tProviderStatus: " + providerStatus);
            tui.Refresh();

            // Get the provider's name.
            string providerName = InputController.ReadText(1, 25, "Provider Name");

            // Show the provider's name.
            tui.WriteLine("\tProviderName: " + providerName);
            tui.Refresh();

            // Get the provider's address.
            string providerAddress = InputController.ReadText(1, 25, "Provider Address");

            // Show the provider's address.
            tui.WriteLine("\tProviderAddress: " + providerAddress);
            tui.Refresh();

            // Get the provider's city.
            string providerCity = InputController.ReadText(1, 14, "Provider City");

            // Show the provider's city.
            tui.WriteLine("\tProviderCity: " + providerCity);
            tui.Refresh();

            // Get the provider's state.
            string providerState = InputController.ReadText(2, 2, "Provider State");

            // Show the provider's state.
            tui.WriteLine("\tProviderState: " + providerState);
            tui.Refresh();

            // Get the provider's zip code.
            string providerZip = InputController.ReadInteger(
                5, 5, true, "Provider Zip").ToString();

            // Show the provider's zip.
            tui.WriteLine("\tProviderZip: " + providerZip);
            tui.Refresh();

            // Get the provider's zip email address.
            string providerEmail = InputController.ReadText(2, 254, "Provider Email");

            // Show the provider's email.
            tui.WriteLine("\tProviderEmail: " + providerEmail);
            tui.Refresh();

            // Get the provider's zip email address.
            string providerPassword = InputController.ReadText(3, 15, "Provider Password");

            // Show the provider's password.
            tui.WriteLine("\tProviderPassword: " + providerPassword);
            tui.Refresh();

            // Create and return the new provider packet.
            return new ProviderPacket(
                action, sessionID, providerID, providerStatus, providerName, providerAddress, 
                providerCity, providerState, providerZip, providerEmail, providerPassword);
        }

        /// <summary>
        /// Attemps to read input from the user to fill out details in an invoice packet.
        /// </summary>
        /// <param name="tui">
        /// The text ui reference for outout to the screen space.
        /// </param>
        /// <param name="action">
        /// The action the packet will perform.
        /// </param>
        /// <param name="sessionID">
        /// The sessionID of the current user.
        /// </param>
        /// <param name="userID">
        /// The userID for the current user.
        /// </param>
        /// <returns>
        /// A newly created invoice packet from user input.
        /// </returns>
        private InvoicePacket ReadInvoicePacket(TextUI tui, string action, 
            string sessionID, string userID)
        {
            // A nice message at the top.
            tui.WriteLine("\tPlease enter the Invoices's details. \n ");
            tui.Render();
            
            // Get the current date / time.
            string currentDateTime = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
            
            // Show the current date / time.
            tui.WriteLine("\tCurrentDateTime: " + currentDateTime);
            tui.Refresh();
            
            string serviceDate = "";

            // We need to get the service date from the user in the correct format.
            while (!DateTime.TryParse(serviceDate, out DateTime result))
            { 
                // Attempt to get a date input from the user and convert it to the correct format.
                serviceDate = InputController.ReadText(10, 10, "Service Date of form MM-DD-YYYY");
                DateTime.TryParse(serviceDate, out result);
                serviceDate = result.ToString("MM-dd-yyyy");
            }

            // Show the service date.
            tui.WriteLine("\tServiceDate: " + serviceDate);
            tui.Refresh();

            // Get the member id this invoice corresponds with.
            string memberID = InputController.ReadInteger(9, 9, true, "Member ID").ToString();

            // Show the member id.
            tui.WriteLine("\tMemberID: " + memberID);
            tui.Refresh();

            // Get the service code the invoice will charge for.
            string serviceCode = InputController.ReadInteger(6, 6, true, "Service Code").ToString();

            // Show the service code.
            tui.WriteLine("\tServiceCode: " + serviceCode);
            tui.Refresh();

            // Get any comments from the user.
            string comments = InputController.ReadText(0, 100, "Comments").ToString();

            // Show the comments.
            tui.WriteLine("\tComments: " + comments);
            tui.Refresh();

            // Create and return the new invoice packet.
            return new InvoicePacket(action, sessionID, currentDateTime, serviceDate, userID, 
                memberID, serviceCode, comments);
        }

        /// <summary>
        /// Attemps to read input from the user to fill out details in a login packet.
        /// </summary>
        /// <param name="tui">
        /// The text ui reference for outout to the screen space.
        /// </param>
        /// <param name="accessLevel">
        /// The access level for the packet to contain.
        /// </param>
        /// <returns>
        /// A newly created login packet from user input.
        /// </returns>
        private LoginPacket ReadLoginPacket(TextUI tui, int accessLevel)
        {
            // Show some nice message at the top.
            tui.Render();
            tui.WriteLine(" \nPlease enter login details \n ", TextUI.TextUIJustify.CENTER);
            tui.Refresh();

            // Get the username from the user. It must be a 9 digit, positive value.
            string username = InputController.ReadInteger(9, 9, true, "User ID").ToString();
            
            // Show the username.
            tui.WriteLine(String.Format("UserID: {0}", username), TextUI.TextUIJustify.CENTER);
            tui.Refresh();

            // Get the password from the user.
            string password = InputController.ReadText(1, 20, "Password");
            
            // Show the password.
            tui.WriteLine(String.Format("Password: {0}", password), TextUI.TextUIJustify.CENTER);
            tui.Refresh();

            // Create and return a new login packet.
            return new LoginPacket("LOGIN", "", username, password, accessLevel);
        }
        
        /// <summary>
        /// Attemps to read input from the user to fill out details in an service code packet.
        /// </summary>
        /// <param name="tui">
        /// The text ui reference for outout to the screen space.
        /// </param>
        /// <param name="action">
        /// The action the packet will perform.
        /// </param>
        /// <param name="sessionID">
        /// The sessionID of the current user.
        /// <returns>
        /// A newly created service code packet from user input.
        /// </returns>
        private ServiceCodePacket ReadServiceCodePacket(TextUI tui, string action, string sessionID)
        {
            // A nice message at the top.
            tui.WriteLine(" \n \nPlease enter the service code details \n ", TextUI.TextUIJustify.CENTER);
            tui.Refresh();

            // Some default service code we don't care about.
            string serviceCode = "000001";

            if (action == "UPDATE_SERVICE_CODE")
            {
                // Get the service code, it must be a 6 digit, positive number.
                serviceCode = InputController.ReadInteger(6, 6, true, "Service Code").ToString();

                // Show the service code
                tui.WriteLine("\tServiceCode: " + serviceCode);
                tui.Refresh();
            }

            // Get the provider id from user input. It must be a 9 digit, positive number.
            string providerID = InputController.ReadInteger(9, 9, true, "Provider ID").ToString();

            // Show the provider id.
            tui.WriteLine("\tProviderID: " + providerID);
            tui.Refresh();

            // Get the service name from user input.
            string serviceName = InputController.ReadText(0, 20, "Service Name").ToString();

            // Show the service name.
            tui.WriteLine("\tServiceName: " + serviceName);
            tui.Refresh();

            string fee = "";
            float outFee = 999999.9f;

            // Try to get a floating point value from user input. It must be less than 999.99;
            while (!float.TryParse(fee, out outFee) || outFee > 999.99f)
            { 
                fee = InputController.ReadText(1, 6, "Service Fee (0.0)").ToString();
            }

            // Show the service fee.
            tui.WriteLine("\tServiceFee: " + fee);
            tui.Refresh();

            // Create and return a new service code packet.
            return new ServiceCodePacket(
                action, sessionID, providerID, outFee, serviceCode, serviceName);
        }

        /// <summary>
        /// Attemps to read input from the user to fill out details in an date range packet.
        /// </summary>
        /// <param name="tui">
        /// The text ui reference for outout to the screen space.
        /// </param>
        /// <param name="action">
        /// The action the packet will perform.
        /// </param>
        /// <param name="sessionID">
        /// The sessionID of the current user.
        /// <returns>
        /// A newly created date range packet from user input.
        /// </returns>
        private DateRangePacket ReadDateRangePacket(TextUI tui, string action, string sessionID)
        {
            // A nice message at the top.
            tui.WriteLine("Please enter custom report details \n ", TextUI.TextUIJustify.CENTER);
            tui.Refresh();

            // Get the 9 digit, positive ID for this packet. The context of the ID will is based
            // on the option the user chose. Either member or provider, the state will be displayed
            // at the top header.
            string id = InputController.ReadInteger(9, 9, true, "ID").ToString();

            // Show the id.
            tui.WriteLine("\tID: " + id);
            tui.Refresh();
    
            // Get the end date and start date range from the current date.
            string endDate = DateTime.Now.ToString("MM-dd-yyyy");
            
            DateTime.TryParse(endDate, out DateTime result);
            string startDate = result.AddDays(-5.0).ToString("MM-dd-yyyy");
            
    
            // Show start date.
            tui.WriteLine("\tStartDate: " + startDate.ToString());
            tui.Refresh();

            // Show end date.
            tui.WriteLine("\tEndDate: " + endDate);
            tui.Refresh();

            tui.WriteLine("Press any key to confirm.");
            tui.Refresh();

            // Press any key to continue.
            Console.ReadKey();

            // Create and return a new date range packet.
            return new DateRangePacket(action, sessionID, startDate.ToString(), endDate, id);
        }
    }
}
