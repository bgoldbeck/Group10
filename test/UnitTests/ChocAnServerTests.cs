﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ChocAnServer;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class ChocAnServerTests
    {
        /// <summary>
        /// Helper method for generating test invoice packets
        /// </summary>
        /// <returns></returns>
        private InvoicePacket GenerateTestInvoicePacket()
        {
            return new InvoicePacket("ADD_INVOICE", "1234", "12-12-2017 12:12:12",
                    "12-12-2017", "123456789", "123456789", "123456", "Test Comments");
        }

        [TestMethod]
        public void TestProcessAction()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            return;
        }

        [TestMethod]
        public void TestGetMD5Hash()
        {
            string str = "abcdefg";
            string md5 = "7ac66c0f148de9519b8bd264312c4d64";
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            PrivateObject obj = new PrivateObject(server);
            string retVal = obj.Invoke("GetMD5Hash", str).ToString();

            Assert.AreEqual(retVal, md5);
            return;
        }

        [TestMethod]
        public void TestRequestAddMember()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            return;
        }

        [TestMethod]
        public void TestRequestMemberStatus()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();

            //server.ProcessAction(new MemberPacket(1234, "MEMBER_STATUS"));

            return;
        }

        /// <summary>
        /// Making sure an exception is not thrown for converting from BasePacket
        /// </summary>
        [TestMethod]
        public void TestRequestAddInvoice()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            ResponsePacket packet = server.ProcessAction(GenerateTestInvoicePacket());
        }

        // Write test to verify response is not null for RequestAddInvoice
    }
}
