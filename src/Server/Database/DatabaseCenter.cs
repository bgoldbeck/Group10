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
        private SQLiteConnection dbConnection;
        private SQLiteCommand dbCommand;

        private DatabaseCenter()
        {
            filepath = "";
            dbConnection = null;
            dbCommand = null;
        }

        public static DatabaseCenter Singelton
        {
            get
            {  
                return instance;
            }
        }

        public bool Initialize(string filepath = "database.db")
        {
            bool success = true;
            try { 
                if (!File.Exists(filepath))
                {
                    SQLiteConnection.CreateFile(filepath);
                }

                dbConnection = new SQLiteConnection("URI=file:" + filepath);
                dbConnection.Open();
            }
            catch (SQLiteException exception)
            {
                Console.WriteLine("SQL could not open : " + filepath + " because " + exception.Message);
                success = false;
            }

            return success;
        }


    }

}