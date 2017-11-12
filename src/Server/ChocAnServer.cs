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

            // Generic response if no action executes.
            ResponsePacket responsePacket = new ResponsePacket(
                basePacket.Action(), basePacket.SessionID(), "", "No Action Performed.");

            switch (basePacket.Action())
            {
                case "ADD_MEMBER":
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
                case "MEMBER_STATUS":
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

        //If session is valid return userid, otherwise empty string.
        private string GetUserIDBySession(string sessionKey)
        {
            if (sessionKey == null)
            {
                throw new ArgumentNullException("sessionKey", "Argument passed in was null, expected string type.");
            }
            
            object[][] sessionTable = database.ExecuteQuery(String.Format("SELECT * FROM sessions " +
                "WHERE sessionKey = '{0}' LIMIT 1;", sessionKey), out int affectedRecords);
            
            if(sessionTable.Length != 0 && sessionTable[0].Length != 0)
            {
                DateTime timeNow = new DateTime(DateTime.Now.Ticks);

                if (DateTime.TryParse(sessionTable[0][2].ToString(), out DateTime sessTime))
                {
                    //If the time now is before the session expiration...
                    if (timeNow.CompareTo(sessTime) <= 0)
                        return sessionTable[0][1].ToString();
                }


            }

            return "";
        }

        private ResponsePacket RequestLogin(LoginPacket packet)
        {
            if(packet == null)
            {
                // Exception.
            }

            string sessionID = "";
            string response = "";

            object[][] userdata = database.ExecuteQuery(String.Format("SELECT * FROM users " +
                "WHERE userID = {0} LIMIT 1;", packet.ID()), out int affectedRecords);

            /*string resp = "";

            for (int i = 0; i < data[0].Length; i++)
            {
                resp += String.Format("{0} ", data[0][i]);
            }*/

            if (userdata.Length == 0)
                response = "Login Failed: Invalid UserID";
            else if (!userdata[0][1].ToString().Equals(packet.Password(), StringComparison.Ordinal))
                response = String.Format("Login Failed: Incorrect password {0} != {1}", userdata[0][1].ToString(), packet.Password());
            else if (Convert.ToInt32(userdata[0][2]) != 1)
                response = "Login Failed: Account inactive";
            else if (Convert.ToInt32(userdata[0][4]) != packet.AccessLevel())
                response = "Login Failed: You do not have access to this terminal";
            else
            {
                Random rng = new Random();
                sessionID = GetMD5Hash(rng.Next().ToString());

                DateTime date = new DateTime(DateTime.Now.Ticks);
                date = date.AddHours(18); //Add 18 hours so the session expires 18 hours from NOW.


                object[][] session = database.ExecuteQuery(String.Format("SELECT * FROM sessions " +
                    "WHERE userID = '{0}' LIMIT 1;", userdata[0][0]), out affectedRecords);

                if (session.Length != 0)
                {
                    database.ExecuteQuery(String.Format("UPDATE sessions " +
                        "SET sessionKey = '{0}', expirationTime = '{1}' " +
                        "WHERE userID = '{2}';", sessionID, date.ToString(), userdata[0][0]), out affectedRecords);
                }
                else
                {
                    database.ExecuteQuery(String.Format("INSERT INTO sessions(userID, expirationTime, sessionKey) " +
                        "VALUES( '{0}', '{1}', '{2}' );", userdata[0][0], date.ToString(), sessionID), out affectedRecords);
                }

                response = "Login Successful";
            }
            
            return new ResponsePacket("LOGIN", "", sessionID, response); 
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

            ResponsePacket responsePacket = new ResponsePacket(
                packet.Action(), packet.SessionID(), "", "");

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

        /// <summary>
        /// Build an sql statement from the packet for adding a
        /// new member to the database and execute that statement. 
        /// The function will return a ResponsePacket with information
        /// regarding whether the member was added or not.
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestAddMember(MemberPacket packet)
        {
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected MemberPacket type.");
            }
            
            // The member we are adding will have a valid state of 1, because we are 
            // adding a new member, so of course they are valid.
            int memberValid = 1;

            // Build the query up, the sqlite database will execute this statement.
            string builtQuery = String.Format("INSERT INTO members(" +
                "memberID, memberName, memberAddress, memberCity, memberState, " +
                "memberZip, memberValid, memberEmail, memberStatus) VALUES(" +
                "'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}');", 
                packet.ID(), packet.Name(), packet.Address(), packet.City(), packet.State(),
                packet.Zip(), memberValid, packet.Email(), packet.Status()).ToString();

            // Execute the statement on the database. If any rows were added, meaning
            // the member was added, we can check the affectedRecords variable for a 1 (added) or
            // a 0 (not added).
            database.ExecuteQuery(builtQuery, out int affectedRecords);
            

            // Build the response string depending if we added a member.
            string response = affectedRecords > 0 ? "Member saved on record." : "Failed to save member on record.";
            
            return new ResponsePacket("ADD_MEMBER", packet.SessionID(), affectedRecords.ToString(), response);
        }

        private ResponsePacket RequestMemberStatus(MemberPacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }

            //Get the user id if the session is valid.
            string userID = GetUserIDBySession(packet.SessionID());

            //Some sort of accesslevel authentication is needed now...
            
            // Build the query string from the packet.
            string query = String.Format("SELECT memberStatus FROM members WHERE " +
             "memberID='{0}';", packet.ID().ToString());

            object[][] table = database.ExecuteQuery(query, out int affectedRecords);
            
            string response = "";
            string data = "";

            if (data == null)
            {
                response = "Could not find member in database.";
            }
            else
            {
                response = "Found member in database.";
                data = table[0][0].ToString();
            }

            return new ResponsePacket("MEMBER_STATUS", packet.SessionID(), data, response);
        }

    }
}


