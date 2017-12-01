using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

using ChocAnServer.Packets;
using SQLLiteDatabaseCenter;
using System.IO;

namespace ChocAnServer
{
    public class ChocAnServer
    {
        // Private Class Variables
        private DatabaseCenter database;
        private string logFilepath = "log.txt";

        /// <summary>
        /// Constructor for ChocAnServer Class
        /// </summary>
        public ChocAnServer()
        {
            this.database = DatabaseCenter.Singelton;
            this.database.Initialize();

            // Open the log file, clear the contents, add the datetime to the top, close the file.
            System.IO.File.WriteAllLines(this.logFilepath, 
                new string[] { "Initialize: " + DateTime.Now.ToString() });

            WriteLogEntry(String.Format("{0,-34} : {1,-30} : {2,-50}",
                   "SESSION_ID", "ACTION", "SERVER_RESPONSE"));
        }
        
        /// <summary>
        /// Processes all actions incoming from Client system
        /// Calls correct function and returns proper response packet
        /// </summary>
        /// <param name="basePacket"></param>
        /// <returns>ResponsePacket</returns>
        public ResponsePacket ProcessAction(BasePacket basePacket)
        {
            if (basePacket == null)
            { 
                throw new ArgumentNullException("BasePacket cannot be null!");
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

            // Log any successful activity from manager or provider in database
            if (responsePacket != null && basePacket != null)
            { 
                WriteLogEntry(String.Format("{0,-34} : {1,-30} : {2,-50}",
                    basePacket.SessionID() , basePacket.Action(), responsePacket.Response()));
            }
            return responsePacket;
        }

        /// <summary>
        /// Get MD5 hash for sessionID
        /// </summary>
        /// <param name="input"></param>
        /// <returns>string</returns>
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
            int memberID = -1;

            // Build the query up, the sqlite database will execute this statement.
            string query = String.Format("INSERT INTO members(" +
                "memberName, memberAddress, memberCity, memberState, " +
                "memberZip, memberValid, memberEmail, memberStatus) VALUES(" +
                "'{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}', '{7}');",
                packet.Name(), packet.Address(), packet.City(), packet.State(),
                packet.Zip(), memberValid, packet.Email(), packet.Status()).ToString();

            // Execute the statement on the database. If any rows were added, meaning
            // the member was added, we can check the affectedRecords variable for a 1 (added) or
            // a 0 (not added).
            database.ExecuteQuery(query, out int affectedRecords);

            query = String.Format("SELECT memberID FROM members WHERE " +
                    "memberName='{0}' AND memberAddress='{1}' AND memberCity='{2}' AND " +
                    "memberState='{3}' AND memberZip='{4}' AND memberEmail='{5}';",
                    packet.Name(), packet.Address(), packet.City(), packet.State(), 
                    packet.Zip(), packet.Email());

            // Build the response string depending if we added a member.
            string response = affectedRecords > 0 ? "Member saved on record." : "Failed to save member on record.";

            object[][] table = database.ExecuteQuery(query, out affectedRecords);

            memberID = Convert.ToInt32(table[0][0]);

            return new ResponsePacket(
                packet.Action(), packet.SessionID(), memberID.ToString(), response);
        }

        /// <summary>
        /// Adds new provider to database
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>ResponsePacket</returns>
        private ResponsePacket RequestAddProvider(ProviderPacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }

            string response = "";
            int providerID = -1;

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

                providerID = Convert.ToInt32(table[0][0]);

                query = String.Format("INSERT INTO users (userID, passwordKey, isActive, status, accessLevel)" + " VALUES(" +
                    "'{0}', '{1}', {2}, '{3}', {4});", providerID, packet.Password(), 1, "Active", 0);

                database.ExecuteQuery(query, out affectedRecords);

                response = affectedRecords == 0 ? "Could not add provider." : "Provider added to records.";
            }


            ResponsePacket responsePacket = new ResponsePacket(
                packet.Action(), packet.SessionID(), providerID.ToString(), response);

            return responsePacket;
        }

        /// <summary>
        /// Adds new service to database
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>ResponsePacket</returns>
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
        /// Deactivates member in database
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>ResponsePacket</returns>
        private ResponsePacket RequestRemoveMember(MemberPacket packet)
        {
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected MemberPacket type.");
            }

            // Build the query up, the sqlite database will execute this statement.
            string builtQuery = String.Format("UPDATE members SET " +
                "memberValid = '0' WHERE " +
                "memberID = {0};", packet.ID());

            // Execute the statement on the database. If any rows were changed, meaning
            // the member was updated, we can check the affectedRecords variable for a 1 (updated) or
            // a 0 (not updated).
            database.ExecuteQuery(builtQuery, out int affectedRecords);

            // Build the response string depending if we added a member.
            string response = affectedRecords > 0 ? "Member updated on record." : "Failed to update member on record.";

            return new ResponsePacket(packet.Action(), packet.SessionID(), affectedRecords.ToString(), response);
        }

        /// <summary>
        /// Deactivates Provider in database
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>ResponsePacket</returns>
        private ResponsePacket RequestRemoveProvider(ProviderPacket packet)
        {
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected MemberPacket type.");
            }

            // Build the query up, the sqlite database will execute this statement.
            string builtQuery = String.Format("UPDATE providers SET " +
                "providerValid = '0' WHERE " +
                "providerID = {0};", packet.ID());

            // Execute the statement on the database. If any rows were changed, meaning
            // the member was updated, we can check the affectedRecords variable for a 1 (updated) or
            // a 0 (not updated).
            database.ExecuteQuery(builtQuery, out int affectedRecords);

            // Build the response string depending if we added a member.
            string response = affectedRecords > 0 ? "Provider updated on record." : "Failed to update provider on record.";

            return new ResponsePacket(packet.Action(), packet.SessionID(), affectedRecords.ToString(), response);
        }

        /// <summary>
        /// Generates a custom member report based on date range
        /// and member ID passed in.
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>ResponsePacket</returns>
        private ResponsePacket RequestCustomMemberReport(DateRangePacket packet)
        {
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected DateRangePacket type.");
            }
            string response = "";

            // Attempt to get the member by the id from the packet.
            object[][] member = database.ExecuteQuery(String.Format(
                "SELECT * FROM members WHERE memberID='{0}'", packet.ID()), out int affectedRecords);

            if (member == null || member.Length == 0)
            {
                response = "Member does not exist";
            }
            else
            {
                // Execute the query to gather the invoices.
                string query = String.Format("SELECT * FROM invoices " +
                    "WHERE serviceDate BETWEEN '{0}' AND '{1}' AND memberID='{2}'; ",
                    packet.DateStart(), packet.DateEnd(), packet.ID());

                object[][] invoices = database.ExecuteQuery(query, out affectedRecords);
                if (invoices == null || invoices.Length == 0)
                {
                    response = "Member has no services provided to them in the past week.";
                }
                else
                { 
                    // A list of string lines to eventually output to the report file.
                    List<string> lines = new List<string>();
                    // Output, to the top of the report, the member's information we have on 
                    // record.
                    lines.Add(String.Format("Member : {0} \nMember Identification : {1} \n" +
                        "Member Address : {2}, {3}, {4} {5}", member[0][1].ToString(), 
                        Convert.ToInt32((member[0][0])), member[0][2].ToString(), 
                        member[0][3].ToString(), member[0][4].ToString(), 
                        member[0][5].ToString()));

                    lines.Add("\n\tServices provided to this member..\n");

                    // Loop through each invoice.
                    for (int i = 0; i < invoices.Length; ++i)
                    {
                        // Get the service corresponding to the service provided from this invoice.
                        object[][] service = database.ExecuteQuery(String.Format(
                            "SELECT * FROM provider_directory WHERE serviceCode='{0}';", 
                            invoices[i][4].ToString()), out affectedRecords);
                        
                        // Get the provider corresponding to the service provided from this invoice.
                        object[][] provider = database.ExecuteQuery(String.Format(
                            "SELECT * FROM providers WHERE providerID='{0}';",
                            invoices[i][2].ToString()), out affectedRecords);
                        
                        // Write a new line to output to the report, containing information
                        // from this particular invoice.
                        lines.Add(String.Format("Service Date : {0} \nProvider Name : {1}\n" +
                            "Service Name : {2}\n", invoices[i][5].ToString(), 
                            provider[0][1].ToString(), service[0][2]));
                        
                    }
                    // Write everything to the file.
                    File.WriteAllLines(String.Format("{0}_{1}.txt",
                        member[0][1].ToString(), packet.DateEnd()), lines);

                    // Confirmation in response packet, we did stuff.
                    response = "Member report generated.";
                }
            }

            return new ResponsePacket(packet.Action(), packet.SessionID(), "", response);
        }

        /// <summary>
        /// Generates Custom Provider report based on date range and
        /// Provider ID passed in.
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>ResponsePacket</returns>
        private ResponsePacket RequestCustomProviderReport(DateRangePacket packet)
        {
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected DateRangePacket type.");
            }
            string response = "";

            // Attempt to get the member by the id from the packet.
            object[][] provider = database.ExecuteQuery(String.Format(
                "SELECT * FROM providers WHERE providerID='{0}'", packet.ID()), out int affectedRecords);

            if (provider == null || provider.Length == 0)
            {
                response = "Provider does not exist";
            }
            else
            {
                // Execute the query to gather the invoices.
                object[][] invoices = database.ExecuteQuery(String.Format("SELECT * FROM invoices " +
                    "WHERE serviceDate BETWEEN '{0}' AND '{1}' AND providerID='{2}'; ",
                    packet.DateStart(), packet.DateEnd(), packet.ID()), out affectedRecords);
                if (invoices == null || invoices.Length == 0)
                {
                    response = "Provider has no services provided by them in the past week.";
                }
                else
                {
                    // A list of string lines to eventually output to the report file.
                    List<string> lines = new List<string>();
                    // Output, to the top of the report, the member's information we have on 
                    // record.
                    lines.Add(String.Format("Provider : {0} \nProvider Identification : {1} \n" +
                        "Provider Address : {2}, {3}, {4} {5}", provider[0][1].ToString(),
                        Convert.ToInt32((provider[0][0])), provider[0][2].ToString(),
                        provider[0][3].ToString(), provider[0][4].ToString(),
                        provider[0][5].ToString()));

                    lines.Add("\n\tServices provided by this Provider..\n");

                    float totalFee = 0.0f;
                    
                    // Loop through each invoice.
                    for (int i = 0; i < invoices.Length; ++i)
                    {
                        // Get the service corresponding to the service provided from this invoice.
                        object[][] service = database.ExecuteQuery(String.Format(
                            "SELECT * FROM provider_directory WHERE serviceCode='{0}';",
                            invoices[i][4].ToString()), out affectedRecords);

                        // Get the provider corresponding to the service provided from this invoice.
                        object[][] member = database.ExecuteQuery(String.Format(
                            "SELECT * FROM members WHERE memberID='{0}';",
                            invoices[i][1].ToString()), out affectedRecords);

                        // Write a new line to output to the report, containing information
                        // from this particular invoice.
                        lines.Add(String.Format("Service Date : {0} \nDate Received: {1}\n" +
                            "Member Name : {2}\nMember Identification : {3}\nService Code : {4}" +
                            "\nFee : ${5}\n", invoices[i][5].ToString(), invoices[i][3].ToString(),
                            member[0][1].ToString(), Convert.ToInt32(member[0][0]).ToString(),
                            Convert.ToInt32(service[0][0]).ToString(),
                            Convert.ToDouble(service[0][3]).ToString()));

                        // Accumulate the total fee.
                        totalFee += (float)Convert.ToDouble(service[0][3]);
                    }
                    // Add the total visits and fees to end of the list.
                    lines.Add(String.Format("\nTotal number of visits with members : {0}\n" +
                        "Total fee for the week : {1}", invoices.Length, totalFee));

                    // Write everything to the file.
                    File.WriteAllLines(String.Format("{0}_{1}.txt",
                        provider[0][1].ToString(), packet.DateEnd()), lines);

                    // Confirmation in response packet, we did stuff.
                    response = "Provider report generated.";
                }
            
            }

            return new ResponsePacket(packet.Action(), packet.SessionID(), "", response);
        }

        /// <summary>
        /// Updates an existing member in the database
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>Response Packet</returns>
        private ResponsePacket RequestUpdateMember(MemberPacket packet)
        {
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected MemberPacket type.");
            }

       
            // Build the query up, the sqlite database will execute this statement.
            string query = String.Format("UPDATE members SET " +
                "memberName = '{0}', memberAddress = '{1}', memberCity = '{2}', " +
                "memberState = '{3}', memberZip = '{4}', memberEmail = '{5}', memberStatus = '{6}' WHERE " +
                "memberID = {7}",
                packet.Name(), packet.Address(), packet.City(), 
                packet.State(), packet.Zip(), packet.Email(), packet.Status(), packet.ID());


            // Execute the statement on the database. If any rows were changed, meaning
            // the member was updated, we can check the affectedRecords variable for a 1 (updated) or
            // a 0 (not updated).
            database.ExecuteQuery(query, out int affectedRecords);


            // Build the response string depending if we added a member.
            string response = affectedRecords > 0 ? "Member update on record." : "Failed to update member record.";


            return new ResponsePacket(packet.Action(), packet.SessionID(), affectedRecords.ToString(), response);
        } 
        
        /// <summary>
        /// Updates an existing provider in the database
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>ResponsePacket</returns>
        private ResponsePacket RequestUpdateProvider(ProviderPacket packet)
        {
            if (packet == null)
            {
                // Exception.
            }
            string providerValid = packet.Status() == "ACTIVE" ? "1" : "0";
            string response = "";

            // Build the query up, the sqlite database will execute this statement.
            string query = String.Format("UPDATE providers SET " +
                "providerName='{0}', providerAddress='{1}', providerCity='{2}', providerState='{3}'," +
                "providerZip='{4}', providerEmail='{5}', providerValid='{6}' WHERE providerID='{7}';",
                packet.Name(), packet.Address(), packet.City(), packet.State(), 
                packet.Zip(), packet.Email(), providerValid, packet.ID());

            database.ExecuteQuery(query, out int affectedRecords);
            if (affectedRecords > 0)
            {
                query = String.Format("UPDATE users SET passwordKey='{0}', isActive='{1}', status='{2}' WHERE userID='{3}';",
                    packet.Password(), providerValid, packet.Status(), packet.ID());

                database.ExecuteQuery(query, out affectedRecords);

                // Build the response string depending if we added a member.
                response = affectedRecords > 0 ? "Provider update on record." : "Failed to update Provider record.";
            }

            ResponsePacket responsePacket = new ResponsePacket(
                packet.Action(), packet.SessionID(), "", response);

            return responsePacket;
        }

        /// <summary>
        /// Updates a service code in the database
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>ResponsePacket</returns>
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
        /// Runs the main accounting procedure weekly to produce 
        /// member reports, provider reports, and the ChocAn report
        /// for managers.
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>Response Packet Object</returns>
        private ResponsePacket RequestMainAccountingProcedure(BasePacket packet)
        {
            // If packet passed in is null, throw exception
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected BasePacket type.");
            }

            string response = "Reports generated.";
            string dateEnd = DateTime.Now.ToString("MM-dd-yyyy");
            string dateStart = DateTime.Now.AddDays(-7.0).ToString("MM-dd-yyyy");

            // Execute the query to gather the members.
            object[][] members = database.ExecuteQuery(
                "SELECT * FROM members;", out int affectedRecords);
               
            if (members != null && members.Length > 0)
            {
                // Loop through all the members.
                for (int i = 0; i < members.Length; ++i)
                {
                    // For every member, execute a custom member report.
                    RequestCustomMemberReport(new DateRangePacket("", "", dateStart, dateEnd,
                        Convert.ToInt32(members[i][0]).ToString()));
                }
            }

            // Execute the query to gather the providers.
            object[][] providers = database.ExecuteQuery(
                "SELECT * FROM providers;", out affectedRecords);

            if (providers != null && providers.Length > 0)
            {
                // Loop through all the providers.
                for (int i = 0; i < providers.Length; ++i)
                {
                    // For every provider, execute a custom provider report.
                    RequestCustomProviderReport(new DateRangePacket("", "", dateStart, dateEnd,
                        Convert.ToInt32(providers[i][0]).ToString()));
                }
            }

            List<string> summaryLines = new List<string>();
            List<string> eftLines = new List<string>();
            
            summaryLines.Add("Manager Summary Report\n\n");

            float overallTotalFee = 0.0f;
            int totalActiveProviders = 0;
            int totalConsultations = 0;

            // Now do the manager summary report and the EFT report.
            // For every provider, find their invoices they accumulated through the week.
            // We need to accumulate the total fee for each provider and the grand total
            // for all providers.
            for (int i = 0; i < providers.Length; ++i)
            {
                // Get their invoices.
                object[][] invoices = database.ExecuteQuery(String.Format(
                    "SELECT * FROM invoices " +
                    "WHERE serviceDate BETWEEN '{0}' AND '{1}' AND providerID='{2}';",
                    dateStart, dateEnd, Convert.ToInt32(providers[i][0]).ToString()), 
                    out affectedRecords);


                // Calculate the total fee this provider accumulated.
                float totalProviderFee = 0.0f;
                for (int j = 0; j < invoices.Length; ++j)
                {
                    // Get the service corresponding to the service provided from this invoice.
                    object[][] service = database.ExecuteQuery(String.Format(
                        "SELECT * FROM provider_directory WHERE serviceCode='{0}';",
                        invoices[j][4].ToString()), out affectedRecords);
                    // Accumulate consultations, and provider fee.
                    totalConsultations += 1; // Accumulate all services, from all providers.
                    totalProviderFee += (float)(Convert.ToDouble(service[0][3]));
                }

                // Overall fee is all the provider's fees summed together.
                overallTotalFee += totalProviderFee;

                // Only output a line in the report, if this provider had any invoices for the
                // week.
                if (totalProviderFee > 0.0f)
                {
                    totalActiveProviders += 1; // This provider provided at least a service.
                    // Add the data to the Summary report file.
                    summaryLines.Add(String.Format("Provider Name : {0}\nNumber of consultations" +
                        " : {1}\nTotal fee for the week : {2}\n", providers[i][1].ToString(), 
                        invoices.Length.ToString(), totalProviderFee.ToString()));

                    // Add the data to the EFT file.
                    eftLines.Add(String.Format("{0},{1},{2}", providers[i][1].ToString(),
                        Convert.ToInt32(providers[i][0].ToString()), totalProviderFee.ToString()));
                }

            }
            // Add the final summary to the ending line of the summary report.
            summaryLines.Add(String.Format("\nTotal Active Providers : {0}\n" +
                "Total Number of Consulations : {1}\nOverall Fee : {2}",totalActiveProviders,
                totalConsultations.ToString(), overallTotalFee.ToString()));

            // Output all the lines to the summary report. Erasing previous contents.
            File.WriteAllLines("WeeklyAccountSummary" + dateEnd + ".txt", summaryLines);

            // Output all the lines to the eft report file. Erasing previous contents.
            File.WriteAllLines("EFT" + dateEnd + ".txt", eftLines);
            
            return new ResponsePacket(packet.Action(), packet.SessionID(), "", response);
        }

        /// <summary>
        /// On provider/manager request and scan of memberID, returns member status
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>ResponsePacket</returns>
        private ResponsePacket RequestMemberStatus(MemberPacket packet)
        {
            // If packet passed in is null, throw exception
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected MemberPacket type.");
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

            if (table == null)
            {
                response = "Could not find member in database.";
            }
            else
            {
                response = "Found member in database.";
                if(table.Count() > 0 && table[0].Count() > 0)
                { 
                    data = table[0][0].ToString();
                }
            }

            return new ResponsePacket("MEMBER_STATUS", packet.SessionID(), data, response);
        }
        /// <summary>
        /// On manager request, allows provider directory to be viewed in textUI
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>ResponsePacket</returns>
        private ResponsePacket RequestProviderDirectory(BasePacket packet)
        {
            // If packet passed in is null, throw exception
            if (packet == null)
            {
                // Exception.
                throw new ArgumentNullException("packet", "Argument passed in was null, expected BasePacket type.");
            }
            // Execute the query on the database and get the entire provider directory contents.
            object[][] providerDirectory = database.ExecuteQuery("SELECT * FROM provider_directory;", out int affectedRecords);

            List<string> contents = new List<string>();

            contents.Add(String.Format(" {0,-26}{1,-13}{2,-15}{3,-22}{4,-13}\n", "Provider Name",
                "Service Code", "ProviderID", "Service Name", "Service Fee"));

            for (int i = 0; i < providerDirectory.Length; ++i)
            {
                string query = String.Format("SELECT providerName from providers " +
                    "WHERE providerID={0};", providerDirectory[i][1].ToString());

                object[][] queryResults = database.ExecuteQuery(query, out affectedRecords);
                string providerName = "Unknown Provider Name";
                if (queryResults.Count() > 0)
                { 
                    providerName = queryResults[0][0].ToString();
                }
                string line = String.Format(String.Format(" {0,-26}{1,-13}{2,-15}{3,-22}{4,-13}\n",
                    providerName, 
                    providerDirectory[i][0].ToString(), providerDirectory[i][1].ToString(),
                    providerDirectory[i][2].ToString(), 
                    (Convert.ToDouble(providerDirectory[i][3])).ToString()));
                
                contents.Add(line);

            }

            System.IO.File.WriteAllLines("ProviderDirectory.txt", contents.ToArray());

            return new ResponsePacket(
                packet.Action(), packet.SessionID(), "", "Provider directory request acknowledged");
        }

        /// <summary>
        /// Adds an invoice to the directory
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>ResponsePacket</returns>
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

        /// <summary>
        /// Login procedure
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>ResponsePacket</returns>
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

        /// <summary>
        /// Logs activity from server into database log
        /// </summary>
        /// <param name="entry"></param>
        public void WriteLogEntry(string entry)
        {
            if (entry != null)
            {
                // Write entry as long as it's not null.
                System.IO.File.WriteAllText(this.logFilepath,
                    DateTime.Now.ToString() + " " + entry + "\n");
            }
            
            return;
        }
    }
}


