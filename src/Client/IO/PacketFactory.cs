using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChocAnServer.Packets;
using HealthcareClientSystem;

namespace HealthcareClientSystem.IO
{
    public class PacketFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tui"></param>
        /// <param name="packetType"></param>
        /// <param name="action"></param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public BasePacket BuildPacket(TextUI tui, String packetType, string action, string sessionID = "", string userID = "", int accessLevel = -1)
        {
            BasePacket packet = null;
            
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
                    break;
            }
            return packet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tui"></param>
        /// <param name="action"></param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        private MemberPacket ReadMemberPacket(TextUI tui, string action, string sessionID)
        {
            tui.WriteLine("\tPlease enter the member's details.");

            tui.Render();

            //string memberID = InputController.ReadInteger(9, 9, true, "Member ID").ToString();

            // The member is active because we are adding a NEW member.
            string memberStatus = "ACTIVE";

            //tui.WriteLine("\tMemberID: " + memberID);
            tui.WriteLine("\tMemberActive: " + memberStatus);

            tui.Refresh();

            // Get the member's name.
            string memberName = InputController.ReadText(0, 25, "Member Name");

            tui.WriteLine("\tMemberName: " + memberName);
            tui.Refresh();

            // Get the member's address.
            string memberAddress = InputController.ReadText(0, 25, "Member Address");

            tui.WriteLine("\tMemberAddress: " + memberAddress);
            tui.Refresh();

            // Get the member's city.
            string memberCity = InputController.ReadText(0, 14, "Member City");

            tui.WriteLine("\tMemberCity: " + memberCity);
            tui.Refresh();

            // Get the member's state.
            string memberState = InputController.ReadText(0, 2, "Member State");

            tui.WriteLine("\tMemberState: " + memberState);
            tui.Refresh();

            // Get the member's zip code.
            string memberZip = InputController.ReadInteger(0, 5, true, "Member Zip").ToString();

            tui.WriteLine("\tMemberZip: " + memberZip);
            tui.Refresh();

            // Get the member's zip email address.
            string memberEmail = InputController.ReadText(0, 254, "Member Email");

            tui.WriteLine("\tMemberEmail: " + memberEmail);

            return new MemberPacket(
                action, sessionID, "123456789", 
                memberStatus, memberName, memberAddress,
                memberCity, memberState, memberZip, memberEmail);
        }

        private ProviderPacket ReadProviderPacket(TextUI tui, string action, string sessionID)
        {
            tui.WriteLine("\tPlease enter the provider's details.");

            tui.Render();

            //string providerID = InputController.ReadInteger(9, 9, true, "Provider ID").ToString();

            // The member is active because we are adding a NEW member.
            string providerStatus = "ACTIVE";

            //tui.WriteLine("\tProviderID: " + providerID);
            tui.WriteLine("\tProviderStatus: " + providerStatus);

            tui.Refresh();

            // Get the member's name.
            string providerName = InputController.ReadText(1, 25, "Provider Name");

            tui.WriteLine("\tProviderName: " + providerName);
            tui.Refresh();

            // Get the member's address.
            string providerAddress = InputController.ReadText(1, 25, "Provider Address");

            tui.WriteLine("\tProviderAddress: " + providerAddress);
            tui.Refresh();

            // Get the member's city.
            string providerCity = InputController.ReadText(1, 14, "Provider City");

            tui.WriteLine("\tProviderCity: " + providerCity);
            tui.Refresh();

            // Get the member's state.
            string providerState = InputController.ReadText(2, 2, "Provider State");

            tui.WriteLine("\tProviderState: " + providerState);
            tui.Refresh();

            // Get the member's zip code.
            string providerZip = InputController.ReadInteger(5, 5, true, "Provider Zip").ToString();

            tui.WriteLine("\tProviderZip: " + providerZip);
            tui.Refresh();

            // Get the member's zip email address.
            string providerEmail = InputController.ReadText(2, 254, "Provider Email");

            tui.WriteLine("\tProviderEmail: " + providerEmail);
            
            // Get the member's zip email address.
            string providerPassword = InputController.ReadText(3, 15, "Provider Password");

            tui.WriteLine("\tProviderPassword: " + providerPassword);

            return new ProviderPacket(
                action, sessionID, "123456789",
                providerStatus, providerName, providerAddress,
                providerCity, providerState, providerZip, providerEmail, providerPassword);
        }

        private InvoicePacket ReadInvoicePacket(TextUI tui, string action, string sessionID, string userID)
        {
            tui.WriteLine("\tPlease enter the Invoices's details.");

            tui.Render();

            string invoiceID = InputController.ReadInteger(9, 9, true, "Invoice ID").ToString();
            string currentDateTime = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");

            tui.WriteLine("\tInvoiceID: " + invoiceID);
            tui.WriteLine("\tCurrentDateTime: " + currentDateTime);
            tui.Refresh();


            string serviceDate = "";

            while (!DateTime.TryParse(serviceDate, out DateTime result))
            { 
                serviceDate = InputController.ReadText(10, 10, "Service Date of form MM-DD-YYYY");
            }

            string memberID = InputController.ReadInteger(9, 9, true, "MemberID ID").ToString();

            tui.WriteLine("\tMemberID: " + memberID);
            tui.Refresh();

            string serviceCode = InputController.ReadInteger(6, 6, true, "Service Code").ToString();
            tui.WriteLine("\tServiceCode: " + serviceCode);
            tui.Refresh();

            string comments = InputController.ReadText(0, 100, "Comments").ToString();
            tui.WriteLine("\tComments: " + comments);
            tui.Refresh();

            return new InvoicePacket(
                action, sessionID, currentDateTime, serviceDate, userID, memberID, serviceCode, comments);
        }

        private LoginPacket ReadLoginPacket(TextUI tui, int accessLevel)
        {
            tui.WriteLine("Terminal [Login]", TextUI.TextUIJustify.CENTER);
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

            // Do login packet here.
            return new LoginPacket("LOGIN", "", username, password, accessLevel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tui"></param>
        /// <param name="action"></param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        private ServiceCodePacket ReadServiceCodePacket(TextUI tui, string action, string sessionID)
        {
            tui.WriteLine(" \n \nPlease enter the service code details", TextUI.TextUIJustify.CENTER);
            tui.Render();

            string serviceCodeID = InputController.ReadInteger(6, 6, true, "Service Code ID").ToString();

            tui.WriteLine("\tServiceCodeID: " + serviceCodeID);
            tui.Refresh();

            string serviceName = InputController.ReadText(0, 20, "Service Name").ToString();

            tui.WriteLine("\tServiceName: " + serviceName);
            tui.Refresh();
            string fee = "";
            float outFee = 999999.9f;

            while (!float.TryParse(fee, out outFee) || outFee > 999.99f)
            { 
                fee = InputController.ReadText(1, 6, "Service Fee (0.0)").ToString();
            }

            tui.WriteLine("\tServiceFee: " + fee);
            tui.Refresh();

            return new ServiceCodePacket(action, sessionID, outFee, serviceCodeID, serviceName);
        }

        private DateRangePacket ReadDateRangePacket(TextUI tui, string action, string sessionID)
        {
            tui.WriteLine("Please enter custom report details", TextUI.TextUIJustify.CENTER);

            tui.Render();

            string id = InputController.ReadInteger(9, 9, true, "ID").ToString();

            tui.WriteLine("\tID: " + id);

            string startDate = "";
            while (!DateTime.TryParse(startDate, out DateTime result))
            {
                startDate = InputController.ReadText(10, 10, "Start Date of form MM-DD-YYYY");
            }

            tui.WriteLine("\tStartDate: " + startDate);
            tui.Refresh();

            string endDate = "";
            while (!DateTime.TryParse(endDate, out DateTime result))
            {
                endDate = InputController.ReadText(10, 10, "EndDate Date of form MM-DD-YYYY");
            }

            return new DateRangePacket(action, sessionID, startDate, endDate, id);
        }
    }
}
