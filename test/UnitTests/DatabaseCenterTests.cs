using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLLiteDatabaseCenter;
using System.Collections.Generic;


namespace UnitTests
{
    [TestClass]
    public class DatabaseCenterTests
    {
        const string testSql = "SELECT 1, 'a', 1.2";
        const string testSqlBad = "HAHAHAHHAHAHAHAAHAH HAHAHAH AAHAHAHAH";

        private DatabaseCenter GetInst()
        {
            DatabaseCenter database = DatabaseCenter.Singelton;
            database.Initialize();
            return database;
        }

        [TestMethod]
        public void DatabaseCenter_Initialize_Valid()
        {
            DatabaseCenter database = DatabaseCenter.Singelton;
            Assert.IsNotNull(database);
            Assert.IsTrue(database.Initialize());
        }

        [TestMethod]
        public void DatabaseCenter_Initialize_Invalid()
        {
            DatabaseCenter database = DatabaseCenter.Singelton;
            Assert.IsNotNull(database);
            Assert.IsFalse(database.Initialize(""));
        }

        [TestMethod]
        public void DatabaseCenter_Initialize_CloseThenOpen()
        {
            DatabaseCenter database = DatabaseCenter.Singelton;
            Assert.IsNotNull(database);
            Assert.IsTrue(database.Initialize());
            database.Close();
            Assert.IsTrue(database.Initialize());
        }

        [TestMethod]
        public void DatabaseCenter_ExecuteQuery_Valid()
        {
            DatabaseCenter database = GetInst();
            int affectedRecords = 0;
            var result = database.ExecuteQuery(testSql, out affectedRecords);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DatabaseCenter_ExecuteQuery_Invalid()
        {
            DatabaseCenter database = GetInst();
            int affectedRecords = 0;
            var result = database.ExecuteQuery(testSqlBad, out affectedRecords);
            Assert.IsNull(result);
        }
    }
}
