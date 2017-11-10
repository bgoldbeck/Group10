using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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
                case "LOGIN":
                    if (basePacket is LoginPacket)
                    {
                        responsePacket = RequestLogin((LoginPacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected LoginPacket", basePacket.Action()), "basePacket");
                    }
                    break;
                default:
                    break;
            }

            return responsePacket;
        }

        private string GetMD5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private ResponsePacket RequestLogin(LoginPacket packet)
        {
            if(packet == null)
            {
                // Exception.
            }

            string sessionID = "";
            string response = "";

            object[][] userdata = database.ExecuteQuery(String.Format("SELECT * FROM users WHERE userID = {0} LIMIT 1;", packet.ID()));

            /*string resp = "";

            for (int i = 0; i < data[0].Length; i++)
            {
                resp += String.Format("{0} ", data[0][i]);
            }*/

            if (userdata.Length == 0)
                response = "Login Failed: Invalid UserID";
            else if (!userdata[0][1].ToString().Equals(packet.Password(), StringComparison.Ordinal))
                response = String.Format("Login Failed: Incorrect Password {0} != {1}", userdata[0][1].ToString(), packet.Password());
            else if (Convert.ToInt32(userdata[0][2]) != 1)
                response = "Login Failed: Account Inactive";
            else
            {
                Random rng = new Random();
                sessionID = GetMD5Hash(rng.Next().ToString());

                object[][] session = database.ExecuteQuery(String.Format("SELECT * FROM sessions WHERE userID = '{0}' LIMIT 1;", userdata[0][0]));

                if(session.Length != 0)
                {
                    database.ExecuteQuery(String.Format("UPDATE sessions SET sessionKey = '{0}' WHERE userID = '{1}';", sessionID, userdata[0][0]));
                }
                else
                {
                    database.ExecuteQuery(String.Format("INSERT INTO sessions(userID, expirationTime, sessionKey) " +
                        "VALUES( '{0}', '{1}', '{2}' );", userdata[0][0], "NEVAR", sessionID));
                }

                response = "Login Successful";
            }

            ResponsePacket responsePacket = new ResponsePacket("LOGIN", "", sessionID, response);

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
            

            return new ResponsePacket("MEMBER_STATUS", packet.SessionID(), data[0][0].ToString(), "");
        }

    }
}


