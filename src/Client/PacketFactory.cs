using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChocAnServer.Packets;

namespace HealthcareClientSystem
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
        public BasePacket ReadPacket(TextUI tui, String packetType, string action, string sessionID = "")
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

            string memberID = InputController.ReadNumeric(9, 9, true, "Member ID").ToString();

            // The member is active because we are adding a NEW member.
            string memberStatus = "ACTIVE";

            tui.WriteLine("\tMemberID: " + memberID);
            tui.WriteLine("\tMemberActive: " + memberStatus);

            tui.Refresh();

            // Get the member's name.
            string memberName = InputController.ReadText(1, 25, "Member Name");

            tui.WriteLine("\tMemberName: " + memberName);
            tui.Refresh();

            // Get the member's address.
            string memberAddress = InputController.ReadText(1, 25, "Member Address");

            tui.WriteLine("\tMemberAddress: " + memberAddress);
            tui.Refresh();

            // Get the member's city.
            string memberCity = InputController.ReadText(1, 14, "Member City");

            tui.WriteLine("\tMemberCity: " + memberCity);
            tui.Refresh();

            // Get the member's state.
            string memberState = InputController.ReadText(2, 2, "Member State");

            tui.WriteLine("\tMemberState: " + memberState);
            tui.Refresh();

            // Get the member's zip code.
            string memberZip = InputController.ReadNumeric(5, 5, true, "Member Zip").ToString();

            tui.WriteLine("\tMemberZip: " + memberZip);
            tui.Refresh();

            // Get the member's zip email address.
            string memberEmail = InputController.ReadText(2, 254, "Member Email");

            tui.WriteLine("\tMemberEmail: " + memberEmail);

            return new MemberPacket(
                action, sessionID, memberID, 
                memberStatus, memberName, memberAddress,
                memberCity, memberState, memberZip, memberEmail);
        }

        private ProviderPacket ReadProviderPacket(TextUI tui, string action, string sessionID)
        {
            tui.WriteLine("\tPlease enter the provider's details.");

            tui.Render();

            string providerID = InputController.ReadNumeric(9, 9, true, "Provider ID").ToString();

            // The member is active because we are adding a NEW member.
            string providerStatus = "ACTIVE";

            tui.WriteLine("\tProviderID: " + providerID);
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
            string providerZip = InputController.ReadNumeric(5, 5, true, "Provider Zip").ToString();

            tui.WriteLine("\tProviderZip: " + providerZip);
            tui.Refresh();

            // Get the member's zip email address.
            string providerEmail = InputController.ReadText(2, 254, "Provider Email");

            tui.WriteLine("\tProviderEmail: " + providerEmail);
            
            // Get the member's zip email address.
            string providerPassword = InputController.ReadText(3, 15, "Provider Password");

            tui.WriteLine("\tProviderPassword: " + providerPassword);

            return new ProviderPacket(
                action, sessionID, providerID,
                providerStatus, providerName, providerAddress,
                providerCity, providerState, providerZip, providerEmail, providerPassword);
        }
    }
}
