﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using ChocAnServer.Packets;
using SQLLiteDatabaseCenter;
using System.IO;

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
                /* MANAGER TERMINAL REQUESTS */
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
                case "ADD_SERVICE_CODE":
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
                case "REMOVE_MEMBER":
                    if (basePacket is MemberPacket)
                    {
                        responsePacket = RequestRemoveMember((MemberPacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected MemberPacket", basePacket.Action()), "basePacket");
                    }
                    break;
                case "REMOVE_PROVIDER":
                    if (basePacket is ProviderPacket)
                    {
                        responsePacket = RequestRemoveProvider((ProviderPacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected ProviderPacket", basePacket.Action()), "basePacket");
                    }
                    break;
                case "CUSTOM_MEMBER_REPORT":
                    if (basePacket is DateRangePacket)
                    {
                        responsePacket = RequestCustomMemberReport((DateRangePacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected DateRangePacket", basePacket.Action()), "basePacket");
                    }
                    break;
                case "CUSTOM_PROVIDER_REPORT":
                    if (basePacket is DateRangePacket)
                    {
                        responsePacket = RequestCustomProviderReport((DateRangePacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected DateRangePacket", basePacket.Action()), "basePacket");
                    }
                    break;
                case "UPDATE_MEMBER":
                    if (basePacket is MemberPacket)
                    {
                        responsePacket = RequestUpdateMember((MemberPacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected MemberPacket", basePacket.Action()), "basePacket");
                    }
                    break;
                case "UPDATE_PROVIDER":
                    if (basePacket is ProviderPacket)
                    {
                        responsePacket = RequestUpdateProvider((ProviderPacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected ProviderPacket", basePacket.Action()), "basePacket");
                    }
                    break;
                case "UPDATE_SERVICE_CODE":
                    if (basePacket is ServiceCodePacket)
                    {
                        responsePacket = RequestUpdateServiceCode((ServiceCodePacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected ServiceCodePacket", basePacket.Action()), "basePacket");
                    }
                    break;
                case "MAIN_ACCOUNTING_PROCEDURE":
                    if (basePacket is BasePacket)
                    {
                        responsePacket = RequestMainAccountingProcedure((BasePacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected BasePacket", basePacket.Action()), "basePacket");
                    }
                    break;
                
                /* PROVIDER TERMINAL REQUESTS */
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
                case "VIEW_PROVIDER_DIRECTORY":
                    if (basePacket is BasePacket)
                    {
                        responsePacket = RequestProviderDirectory(basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected BasePacket", basePacket.Action()), "basePacket");
                    }
                    break;
                
                
                /* OPERATOR TERMINAL REQUESTS */
                case "ADD_INVOICE":
                    if (basePacket is InvoicePacket)
                    {
                        responsePacket = RequestAddInvoice((InvoicePacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type, " +
                            "Expected InvoicePacket", basePacket.Action()), "basePacket");
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

            if (responsePacket != null && basePacket != null)
            { 
                WriteLogEntry(String.Format("{0,-34} {1,-15} {2,-50}",
                    basePacket.SessionID() + ",", basePacket.Action() + ",", responsePacket.Response()));
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
                "memberName, memberAddress, memberCity, memberState, " +
                "memberZip, memberValid, memberEmail, memberStatus) VALUES(" +
                "'{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}', '{7}');",
                packet.Name(), packet.Address(), packet.City(), packet.State(),
                packet.Zip(), memberValid, packet.Email(), packet.Status()).ToString();

            // Execute the statement on the database. If any rows were added, meaning
            // the member was added, we can check the affectedRecords variable for a 1 (added) or
            // a 0 (not added).
            database.ExecuteQuery(builtQuery, out int affectedRecords);


            // Build the response string depending if we added a member.
            string response = affectedRecords > 0 ? "Member saved on record." : "Failed to save member on record.";
           
            return new ResponsePacket(packet.Action(), packet.SessionID(), affectedRecords.ToString(), response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestAddProvider(ProviderPacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }

            string response = "";

            string query = String.Format("INSERT INTO providers(" +
                "providerName, providerAddress, providerCity, providerState, providerZip, providerEmail) VALUES(" +
                "'{0}', '{1}', '{2}', '{3}', '{4}', '{5}');",
                packet.Name(), packet.Address(), packet.City(), packet.State(), packet.Zip(), packet.Email());

            database.ExecuteQuery(query, out int affectedRecords);

            if (affectedRecords == 0)
            {
                response = "Could not add provider.";
            }
            else
            {
                query = String.Format("SELECT providerID FROM providers WHERE " +
                    "providerName='{0}' AND providerAddress='{1}' AND providerCity='{2}' AND " +
                    "providerState='{3}' AND providerZip='{4}' AND providerEmail='{5}';",
                    packet.Name(), packet.Address(), packet.City(), packet.State(), packet.Zip(), packet.Email());

                object [][] table = database.ExecuteQuery(query, out affectedRecords);

                int providerID = Convert.ToInt32(table[0][0]);

                query = String.Format("INSERT INTO users (userID, passwordKey, isActive, status, accessLevel)" + " VALUES(" +
                    "'{0}', '{1}', {2}, '{3}', {4});", providerID, packet.Password(), 1, "Active", 0);

                database.ExecuteQuery(query, out affectedRecords);

                response = affectedRecords == 0 ? "Could not add provider." : "Provider added to records.";
            }


            ResponsePacket responsePacket = new ResponsePacket(
                packet.Action(), packet.SessionID(), "", response);

            return responsePacket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestAddServiceCode(ServiceCodePacket packet)
        {
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected MemberPacket type.");
            }

            // Initialize the response string.
            string response = "";
            
            // Build the query up, the sqlite database will execute this statement.
            string query = String.Format("INSERT INTO provider_directory(" +
                "providerID, serviceName, serviceFee) VALUES('{0}', '{1}', '{2}');",
                packet.ProviderID(), packet.Name(), packet.Fee());


            // Execute the statement on the database. If any rows were added, meaning
            // the service was added, we can check the affectedRecords variable for a 1 (added) or
            // a 0 (not added).
            database.ExecuteQuery(query, out int affectedRecords);

            // Build the response string depending if we added a service.
            response = affectedRecords > 0 ? "Service saved on record." : "Failed to save service on record.";
            
            return new ResponsePacket(packet.Action(), packet.SessionID(), "", response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestRemoveMember(MemberPacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }

            ResponsePacket responsePacket = new ResponsePacket(
                packet.Action(), packet.SessionID(), "", "");

            return responsePacket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestRemoveProvider(ProviderPacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }

            ResponsePacket responsePacket = new ResponsePacket(
                packet.Action(), packet.SessionID(), "", "");

            return responsePacket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestCustomMemberReport(DateRangePacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }

            ResponsePacket responsePacket = new ResponsePacket(
                packet.Action(), packet.SessionID(), "", "");

            return responsePacket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestCustomProviderReport(DateRangePacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }

            ResponsePacket responsePacket = new ResponsePacket(
                packet.Action(), packet.SessionID(), "", "");

            return responsePacket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestUpdateMember(MemberPacket packet)
        {
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected MemberPacket type.");
            }

       
            // Build the query up, the sqlite database will execute this statement.
            string builtQuery = String.Format("UPDATE members SET " +
                "memberName = '{0}', memberAddress = '{1}', memberCity = '{2}', " +
                "memberState = '{3}', memberZip = '{4}', memberEmail = '{5}', memberStatus = '{6}' WHERE " +
                "memberID = {7}",
                packet.Name(), packet.Address(), packet.City(), 
                packet.State(), packet.Zip(), packet.Email(), packet.Status(), packet.ID());


            // Execute the statement on the database. If any rows were changed, meaning
            // the member was updated, we can check the affectedRecords variable for a 1 (updated) or
            // a 0 (not updated).
            database.ExecuteQuery(builtQuery, out int affectedRecords);


            // Build the response string depending if we added a member.
            string response = affectedRecords > 0 ? "Member updated on record." : "Failed to update member on record.";


            return new ResponsePacket(packet.Action(), packet.SessionID(), affectedRecords.ToString(), response);
        } 
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestUpdateProvider(ProviderPacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }

            ResponsePacket responsePacket = new ResponsePacket(
                packet.Action(), packet.SessionID(), "", "");

            return responsePacket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestUpdateServiceCode(ServiceCodePacket packet)
        {
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected MemberPacket type.");
            }

            // Initialize the response string.
            string response = "";

            // Build the query up, the sqlite database will execute this statement.
            string query = String.Format("UPDATE provider_directory SET " +
               "providerID = '{0}', serviceName = '{1}', serviceFee = '{2}' WHERE serviceCode = '{3}';",
               packet.ProviderID(), packet.Name(), packet.Fee(), packet.ID());


            // Execute the statement on the database. If any rows were added, meaning
            // the service was updated, we can check the affectedRecords variable for a 1 (added) or
            // a 0 (not added).
            database.ExecuteQuery(query, out int affectedRecords);

            // Build the response string depending if we added a service.
            response = affectedRecords > 0 ? "Service saved on record." : "Failed to save service on record.";

            return new ResponsePacket(packet.Action(), packet.SessionID(), "", response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestMainAccountingProcedure(BasePacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }
            ResponsePacket responsePacket = null;

            return responsePacket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestProviderDirectory(BasePacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }

            // Execute the query on the database and get the entire provider directory contents.
            object[][] data = database.ExecuteQuery("SELECT * FROM provider_directory;", out int affectedRecords);

            List<string> contents = new List<string>();

            for (int i = 0; i < data.Length; ++i)
            {
                string query = String.Format("SELECT providerName from providers where providerID={0};", data[i][1].ToString());

                string line = "\t" + database.ExecuteQuery(query, out affectedRecords)[0][0].ToString();
                    
                for (int j = 0; j < data[0].Length; ++j)
                {
                    if (j == data[0].Length - 1)
                    {
                        line += ", " + Convert.ToDouble(data[i][j]);
                    }
                    else
                    {
                        line += ", " + data[i][j];
                    }
                    
                }
                contents.Add(line);

            }

            System.IO.File.WriteAllLines("ProviderDirectory.txt", contents.ToArray());

            return new ResponsePacket(
                packet.Action(), packet.SessionID(), "", "Provider directory request acknowledged");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResponsePacket RequestAddInvoice(InvoicePacket packet)
        {
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected InvoicePacket type.");
            }
            // Build the query up, the sqlite database will execute this statement.
            string builtQuery = String.Format("INSERT INTO invoices(" +
                "memberID, providerID, timestamp, serviceCode, serviceDate, comments) VALUES(" +
                "'{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                packet.MemberID(), packet.ProviderID(), packet.CurrentDateTime(), packet.ServiceCode(),
                packet.DateServiceProvided(), packet.Comments()).ToString();

            // Execute the statement on the database. If any rows were added, meaning
            // the invoice was added, we can check the affectedRecords variable for a 1 (added) or
            // a 0 (not added).
            database.ExecuteQuery(builtQuery, out int affectedRecords);

            // Build the response string depending if we added an invoice.
            string response = affectedRecords > 0 ? "Invoice saved on record." : "Failed to save invoice on record.";
            
            return new ResponsePacket(packet.Action(), packet.SessionID(), affectedRecords.ToString(), response);
        }


        private ResponsePacket RequestLogin(LoginPacket packet)
        {
            if (packet == null)
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

            WriteLogEntry(String.Format("{0,-34} {1,-15} {2,-50}",
                sessionID + ",", packet.Action() + ",", response));

            return new ResponsePacket("LOGIN", "", sessionID, response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public void WriteLogEntry(string entry)
        {
            if (entry != null)
            {
                // Write entry as long as it's not null.

                System.IO.File.AppendAllText(this.logFilepath,
                    DateTime.Now.ToString() + " : " + entry);
            }
            
            return;
        }
    }
}


