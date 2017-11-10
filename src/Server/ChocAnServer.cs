using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChocAnServer.Packets;
using SQLLiteDatabaseCenter;

namespace ChocAnServer
{
    public class ChocAnServer
    {
        private DatabaseCenter database;
        private string logFilepath = "log.txt";

        public ChocAnServer()
        {
            this.database = DatabaseCenter.Singelton;
            this.database.Initialize();

            // Open the log file, clear the contents, add the datetime to the top, close the file.
            System.IO.File.WriteAllLines(this.logFilepath, 
                new string[] { "Initialize: " + DateTime.Now.ToString() });
        }

        ~ChocAnServer()
        {
        }

        public ResponsePacket ProcessAction(BasePacket basePacket)
        {
            if (basePacket == null)
            {
                // Throw argument exception.
            }

            ResponsePacket responsePacket = null;

            switch (basePacket.Action())
            {
                case "ADD_MEMBER":
                    if (basePacket is MemberPacket)
                    { 
                        responsePacket = RequestMemberStatus((MemberPacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected MemberPacket", basePacket.Action()), "basePacket");
                    }
                    break;
                case "MEMBER_STATUS":
                    if (basePacket is MemberPacket)
                    {
                        responsePacket = RequestAddMember((MemberPacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected MemberPacket", basePacket.Action()), "basePacket");
                    }
                    break;
                case "ADD_INVOICE":
                    if(basePacket is InvoicePacket)
                    {
                        responsePacket = RequestAddInvoice((InvoicePacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected InvoicePacket", basePacket.Action()), "basePacket");
                    }
                    break;
                case "ADD_SERVICECODE":
                    if (basePacket is ServiceCodePacket)
                    {
                        responsePacket = RequestAddServiceCode((ServiceCodePacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected ServiceCodePacket", basePacket.Action()), "basePacket");
                    }
                    break;
                case "ADD_PROVIDER":
                    if (basePacket is ProviderPacket)
                    {
                        responsePacket = RequestAddProvider((ProviderPacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected ProviderPacket", basePacket.Action()), "basePacket");
                    }
                    break;
                default:
                    break;
            }

            return responsePacket;
        }

        private ResponsePacket RequestAddServiceCode(ServiceCodePacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }
            ResponsePacket responsePacket = null;

            return responsePacket;
        }

        private ResponsePacket RequestAddProvider(ProviderPacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }
            ResponsePacket responsePacket = null;

            return responsePacket;
        }

        private ResponsePacket RequestAddInvoice(InvoicePacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }
            ResponsePacket responsePacket = null;

            return responsePacket;
        }

        private ResponsePacket RequestAddMember(MemberPacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }
            ResponsePacket responsePacket = null;
            
            // These values must be filled later with packet data.
            database.ExecuteQuery("INSERT INTO members(memberID, memberName, memberAddress, memberCity, " +
                "memberState, memberZip, memberValid, memberEmail, memberStatus) VALUES( " +
                "111111111, 'Jordan Green', '666 Devil's Way', 'PDX Close Enough', 'OR', " +
                "'95555', 1, 'JG@PDX.PSU.EDU', 'ACTIVE'" +
                ");");
            // We dont care what the database returns in this case.
            
            return responsePacket;
        }

        private ResponsePacket RequestMemberStatus(MemberPacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }
            
            // Build the query string from the packet.
            string query = "SELECT memberStatus FROM members WHERE " +
             "memberID='" + packet.ID().ToString() + "'" +
             ";";
            
            object[][] data = database.ExecuteQuery(query);
            

            return new ResponsePacket("MEMBER_STATUS", packet.SessionID(), data, "");
        }

    }
}


