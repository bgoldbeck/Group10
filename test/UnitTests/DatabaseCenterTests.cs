using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLLiteDatabaseCenter;
using System.Collections.Generic;
using System.Data.SQLite;

namespace UnitTests
{
    [TestClass]
    public class DatabaseCenterTests
    {
        const string testSql = "SELECT 1";
        const string testSqlBad = "HAHAHAHHAHAHAHAAHAH HAHAHAH AAHAHAHAH";

        private DatabaseCenter GetInst()
        {
            DatabaseCenter database = DatabaseCenter.Singelton;
            database.Initialize();
            return database;
        }

        [TestMethod]
        public void DatabaseCenterInitialize()
        {
            DatabaseCenter database = DatabaseCenter.Singelton;
            Assert.IsNotNull(database);
            Assert.IsTrue(database.Initialize());
        }

        [TestMethod]
        public void DatabaseCenterRunNonQuery()
        {
            DatabaseCenter database = GetInst();
            bool result = database.ExecuteNonQuery(testSql, new List<SQLiteParameter>());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DatabaseCenterRunReaderQuery()
        {
            
            DatabaseCenter database = GetInst();
            SQLiteDataReader dataReader = database.ExecuteReaderQuery(testSql, new List<SQLiteParameter>());
            Assert.IsNotNull(dataReader);
        }

        [TestMethod]
        public void DatabaseCenterRunBadReaderQuery()
        {
            DatabaseCenter database = GetInst();
            SQLiteDataReader dataReader = database.ExecuteReaderQuery(testSqlBad, new List<SQLiteParameter>());
            Assert.IsNull(dataReader);
        }

        [TestMethod]
        public void DatabaseCenterRunBadNonQuery()
        {
            DatabaseCenter database = GetInst();
            bool result = database.ExecuteNonQuery(testSqlBad, new List<SQLiteParameter>());
            Assert.IsFalse(result);
        }
    }
}
