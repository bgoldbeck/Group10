using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SQLLiteDatabaseCenter
{
    /// <summary>
    /// The DatabaseCenter links the server to the database layer.
    /// It’s sole responsibility is communicating with the database.
    /// The database center will execute generic queries to retrieve,
    /// store, and format information from/to the database.
    /// </summary>
    public class DatabaseCenter
    {
        private static readonly DatabaseCenter Instance = new DatabaseCenter();
        
        private SQLiteConnection connection;

        private DatabaseCenter()
        {
            connection = null;
        }

        /// <summary>
        /// Accessor for the singleton method
        /// </summary>
        public static DatabaseCenter Singelton
        {
            get
            {  
                return Instance;
            }
        }

        /// <summary>
        /// Closes the connection to the SQlite database
        /// </summary>
        public void Close()
        { 
          
            if (connection != null)
            { 
                connection.Close();
            }
            return;
        }
        
        /// <summary>
        /// Creates a connection to the SQLite database
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public bool Initialize(string filepath = "db.sqlite")
        {
            bool success = true;
            try
            { 
                if (!File.Exists(filepath))
                {
                    Console.WriteLine("Creating sqlite file.");
                    SQLiteConnection.CreateFile(filepath);
                }

                this.connection = new SQLiteConnection("URI=file:" + filepath);
                this.connection.Open();
            }
            catch (Exception exception)
            {
                Console.WriteLine("SQL could not open : " + filepath + " because " + exception.Message);
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Main method for executing a query against the SQLite database
        /// </summary>
        /// <param name="query">The SQL to run on the server</param>
        /// <param name="affectedRecords">int to say how many records are affected</param>
        /// <returns></returns>
        public object[][] ExecuteQuery(string query, out int affectedRecords)
        {
            // Selects a single Item
            if (query == null)
            {
                // Throw exception.
                affectedRecords = 0;
                throw new ArgumentNullException(query, "Argument passed in was null, expected string type.");
            }

            // Object table to return.
            object[][] ret = null;
            affectedRecords = 0;

            try
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = query;

                SQLiteDataReader reader = command.ExecuteReader();
                ret = SQLRetrieveTableFromReader(reader);
                affectedRecords = reader.RecordsAffected;

                reader.Close();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("SQLiteExcpetion: " + query + "\n"+ e.ToString());
            }

            return ret;
        }

        /// <summary>
        /// Using a DataReader object, this iterates thru it and returns an object array
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private object[][] SQLRetrieveTableFromReader(SQLiteDataReader reader)
        {
            List<List<object>> retrieval = new List<List<object>>();

            while (reader.Read())
            {
                List<object> row = new List<object>();

                for (int col = 0; col < reader.FieldCount; ++col)
                {
                    row.Add(SQLReadFromType(reader.GetFieldType(col).ToString(), reader, col));
                }
                retrieval.Add(row);

            }
            // Linq returning multidimensional array from List<List<>>
            return retrieval.Select(a => a.ToArray()).ToArray();
        }

        /// <summary>
        /// Reads a type from the database and converts it to a C# type
        /// </summary>
        /// <param name="type">The type of data we expect</param>
        /// <param name="dataReader">The SQLiteDataReader</param>
        /// <param name="col"></param>
        /// <returns></returns>
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
                case "System.Double":
                    obj = dataReader.GetDouble(col);
                    break;
                default:
                    break;
            }
            return obj;
        }

    }

}