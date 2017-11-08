using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SQLLiteDatabaseCenter
{
    public class DatabaseCenter
    {
        private static readonly DatabaseCenter instance = new DatabaseCenter();
        
        private string filepath;
        private SQLiteConnection connection;
        private SQLiteDataReader reader;

        private DatabaseCenter()
        {
            filepath = "";
            connection = null;
        }

        public static DatabaseCenter Singelton
        {
            get
            {  
                return instance;
            }
        }

        public void Close()
        {
            if (connection != null)
            { 
                connection.Close();
            }
            if (reader != null)
            { 
                reader.Close();
            }
            return;
        }
//         ^ ^
//        (0,0)
//        (  ()
//        _| _|


        public bool Initialize(string filepath = "db.sqlite")
        {
            bool success = true;
            bool justCreated = false;
            try
            { 
                if (!File.Exists(filepath))
                {
                    Console.WriteLine("Creating sqlite file.");
                    SQLiteConnection.CreateFile(filepath);
                    justCreated = true;
                }

                this.connection = new SQLiteConnection("URI=file:" + filepath);
                this.connection.Open();
            }
            catch (SQLiteException exception)
            {
                Console.WriteLine("SQL could not open : " + filepath + " because " + exception.Message);
                success = false;
            }

            // Create the entire database.
            if (justCreated)
            {
                Console.WriteLine("Building sqlite database.");

                Console.WriteLine("Building sessions.");
                ExecuteQuery("CREATE TABLE IF NOT EXISTS sessions( " +
                    "sessionID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                    "userID TEXT NOT NULL," +
                    "expirationTime TEXT NOT NULL," +
                    "sessionKey TEXT NOT NULL" +
                    ");");

                Console.WriteLine("Building members.");
                ExecuteQuery("CREATE TABLE IF NOT EXISTS members( " +
                    "memberID TEXT PRIMARY KEY NOT NULL," +
                    "memberName TEXT NOT NULL," +
                    "memberAddress TEXT NOT NULL," +
                    "memberCity TEXT NOT NULL," +
                    "memberState TEXT NOT NULL," +
                    "memberZip TEXT NOT NULL," +
                    "memberValid INTEGER NOT NULL," +
                    "memberEmail TEXT NOT NULL," +
                    "memberStatus TEXT NOT NULL" +
                    ");");

                Console.WriteLine("Building providers.");
                ExecuteQuery("CREATE TABLE IF NOT EXISTS providers( " +
                    "providerID TEXT PRIMARY KEY NOT NULL," +
                    "providerName TEXT NOT NULL," +
                    "providerAddress TEXT NOT NULL," +
                    "providerCity TEXT NOT NULL," +
                    "providerState TEXT NOT NULL," +
                    "providerZip TEXT NOT NULL," +
                    "providerEmail TEXT NOT NULL" +
                    ");");

                Console.WriteLine("Building invoices.");
                ExecuteQuery("CREATE TABLE IF NOT EXISTS invoices( " +
                    "invoiceID TEXT PRIMARY KEY NOT NULL," +
                    "memberID TEXT NOT NULL," +
                    "providerID TEXT NOT NULL," +
                    "timestamp TEXT NOT NULL," +
                    "serviceCode INTEGER NOT NULL," +
                    "serviceDate TEXT NOT NULL" +
                    ");");

                Console.WriteLine("Building provider directory.");
                ExecuteQuery("CREATE TABLE IF NOT EXISTS provider_directory( " +
                    "serviceCode TEXT PRIMARY KEY NOT NULL," +
                    "providerID TEXT NOT NULL," +
                    "providerName TEXT NOT NULL," +
                    "serviceName TEXT NOT NULL," +
                    "serviceFee REAL NOT NULL" +
                    ");");

                Console.WriteLine("Building provider user.");
                ExecuteQuery("CREATE TABLE IF NOT EXISTS users( " +
                    "userID TEXT PRIMARY KEY NOT NULL," +
                    "passwordKey TEXT NOT NULL," +
                    "isActive INTEGER NOT NULL," +
                    "status TEXT NOT NULL," +
                    "accessLevel INTEGER NOT NULL" +
                    ");");

                Console.WriteLine("Building provider action_logs.");
                ExecuteQuery("CREATE TABLE IF NOT EXISTS users( " +
                    "logID TEXT PRIMARY KEY NOT NULL," +
                    "timestamp TEXT NOT NULL," +
                    "userID INTEGER NOT NULL," +
                    "action TEXT NOT NULL," +
                    "details TEXT NOT NULL" +
                    ");");

                Console.WriteLine("Inserting dummy data.");
                ExecuteQuery("INSERT INTO members(memberID, memberName, memberAddress, memberCity, " +
                    "memberState, memberZip, memberValid, memberEmail, memberStatus) VALUES( " +
                    "123456789, 'Brandon Goldbeck', '1055 NW Gravel Road', 'Hillsboro', 'OR', " +
                    "'97124', 1, 'bg@psu.edu', 'ACTIVE'" +
                    ");");

                ExecuteQuery("INSERT INTO providers(providerID, providerName, providerAddress, providerCity, " +
                    "providerState, providerZip, providerEmail) VALUES( " +
                    "987654321, 'TrumpCare Inc.', '1600 Amphitheatre Parkway', 'Mountain View', 'CA', " +
                    "'94035', 'tremendous_care@america.com'" +
                    ");");


                ExecuteQuery("INSERT INTO provider_directory(serviceCode, providerID, providerName, " +
                    "serviceName, serviceFee) VALUES( " +
                    "123456, 987654321, 'TrumpCare Inc.', 'Chocolate Rehab', 190.00" +
                    ");");

                ExecuteQuery("INSERT INTO provider_directory(serviceCode, providerID, providerName, " +
                    "serviceName, serviceFee) VALUES( " +
                    "654321, 987654321, 'TrumpCare Inc.', 'Shockolate Therapy', 250.00" +
                    ");");

                ExecuteQuery("INSERT INTO invoices(invoiceID, memberID, providerID, timestamp, serviceCode," +
                    "serviceDate) VALUES( " +
                    "111111111, 123456789, 987654321, '03-15-2017 10:05:02', 654321, '03-12-2017'" +
                    ");");

            }
           
            return success;
        }

        public object[][] ExecuteQuery(string query)
        {
            // Selects a single Item
            if (query == null) return null;

            
            object[][] ret = null;
            try
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = query;

                this.reader = command.ExecuteReader();
                ret = SQLRetrieveFromReader(this.reader);

            }
            catch (SQLiteException e)
            {
                Console.WriteLine("SQLiteExcpetion: " + query + "\n"+ e.ToString());
            }
            return ret;
        }

        private object[][] SQLRetrieveFromReader(SQLiteDataReader dataReader)
        {
            List<List<object>> retrieval = new List<List<object>>();
            while (this.reader.Read())
            {
                List<object> row = new List<object>();

                for (int col = 0; col < this.reader.FieldCount; ++col)
                {
                    row.Add(SQLReadFromType(this.reader.GetFieldType(col).ToString(), this.reader, col));
                }
                retrieval.Add(row);

            }
            // Linq returning multidimensional array from List<List<>>
            return retrieval.Select(a => a.ToArray()).ToArray();
        }

        private object SQLReadFromType(string type, SQLiteDataReader dataReader, int col)
        {
            object obj = null;

            switch (type)
            {
                case "System.String":
                    obj = dataReader.GetString(col);
                    break;
                case "System.Int64":
                    obj = dataReader.GetInt64(col);
                    break;
                case "System.Int32":
                    obj = dataReader.GetInt32(col);
                    break;
                case "System.Single":
                    obj = dataReader.GetFloat(col);
                    break;
                default:
                    break;
            }
            return obj;
        }

    }

}