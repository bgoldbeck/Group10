using System;
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
        private InvoicePacket GenerateTestInvoicePacket(string action)
        {
            return new InvoicePacket(action, "1234", "12-12-2017 12:12:12",
                    "12-12-2017", "123456789", "123456789", "123456", 
                    "Test Comments");
        }

        /// <summary>
        /// Helper method for generating member packets
        /// </summary>
        /// <param name="wrongAction"></param>
        /// <returns></returns>
        private MemberPacket GenerateTestMemberPacket(String action)
        {
            return new MemberPacket(action, "1234", "123456789", "Active",
                "John Doe", "123 HA st", "HACity", "HA", "12345", 
                "hah@hah.hah");
        }

        private MemberPacket GenerateTestRequestMemberPacket(String action)
        {
            return new MemberPacket(action, "1234", "123456789", "Active",
                "John Doe", "123 HA st", "HACity", "HA", "12345",
                "hah@hah.hah");
        }

        /// <summary>
        /// Helper method for generating provider packets
        /// </summary>
        /// <param name="wrongAction"></param>
        /// <returns></returns>
        private ProviderPacket GenerateTestProviderPacket(String action)
        {
            Random random = new Random();
            return new ProviderPacket(action, "5555", "444455555", "Active",
                "NewProvider" + random.Next(0,1000000).ToString(),
                "NewAddress" + random.Next(0, 1000000).ToString(),
                "NewCity", "CI", "12345",
                "heheheh@hehehh.heh", "password");
        }

        /// <summary>
        /// Helper method for generating service code packets
        /// </summary>
        /// <param name="wrongAction"></param>
        /// <returns></returns>
        private ServiceCodePacket GenerateServiceCodePacket(String action)
        {
            return new ServiceCodePacket(action, "1234", "123456",
                123.45f, "123456", "SomeServiceCode");
        }

        /// <summary>
        /// Helper method for generating member reports
        /// </summary>
        /// <param name="wrongAction"></param>
        /// <returns></returns>
        private DateRangePacket GenerateReportDateRangePacket(String action)
        {
            return new DateRangePacket(action, "1234", "11-11-2017", "12-25-2017", "123456788");
        }


        /// <summary>
        /// Passing the wrong action should throw an exception
        /// </summary>
        [TestMethod]
        public void ProcessAction_Invalid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            Assert.ThrowsException<ArgumentNullException>(() => server.ProcessAction(null));
            return;
        }

        /// <summary>
        /// Should produce an accurate MD5 hash
        /// </summary>
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

        /// <summary>
        /// Should return a non-null packet
        /// </summary>
        [TestMethod]
        public void RequestAddInvoice_Valid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            ResponsePacket packet = server.ProcessAction(GenerateTestInvoicePacket("ADD_INVOICE"));
            Assert.IsNotNull(packet);
        }

        /// <summary>
        /// Passing the wrong action should throw an exception
        /// </summary>
        [TestMethod]
        public void RequestAddInvoice_Invalid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            Assert.ThrowsException<ArgumentException>(() =>
                server.ProcessAction(GenerateTestMemberPacket("ADD_INVOICE")));
        }

        /// <summary>
        /// Should return a non-null packet
        /// </summary>
        [TestMethod]
        public void RequestAddMember_Valid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            ResponsePacket packet = server.ProcessAction(GenerateTestMemberPacket("ADD_MEMBER"));
            Assert.IsNotNull(packet);
        }

        /// <summary>
        /// Passing the wrong action should throw an exception
        /// </summary>
        [TestMethod]
        public void RequestAddMember_Invalid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            Assert.ThrowsException<ArgumentException>(() =>
                server.ProcessAction(GenerateTestInvoicePacket("ADD_MEMBER")));
        }

        /// <summary>
        /// Should return a non-null packet
        /// </summary>
        [TestMethod]
        public void RequestAddProvider_Valid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            ResponsePacket packet = server.ProcessAction(GenerateTestProviderPacket("ADD_PROVIDER"));
            Assert.IsNotNull(packet);
        }

        /// <summary>
        /// Passing the wrong action should throw an exception
        /// </summary>
        [TestMethod]
        public void RequestAddProvider_Invalid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            Assert.ThrowsException<ArgumentException>(() =>
                server.ProcessAction(GenerateTestMemberPacket("ADD_PROVIDER")));
        }

        /// <summary>
        /// Should return a non-null packet
        /// </summary>
        [TestMethod]
        public void RequestAddServiceCode_Valid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            ResponsePacket packet = server.ProcessAction(GenerateServiceCodePacket("ADD_SERVICE_CODE"));
            Assert.IsNotNull(packet);
        }

        /// <summary>
        /// Passing the wrong action should throw an exception
        /// </summary>
        [TestMethod]
        public void RequestAddServiceCode_Invalid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            Assert.ThrowsException<ArgumentException>(() =>
                server.ProcessAction(GenerateTestMemberPacket("ADD_SERVICE_CODE")));
        }

        /// <summary>
        /// Should return a non-null packet
        /// </summary>
        [TestMethod]
        public void RequestRequestCustomProvider_Valid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            
            ResponsePacket packet = server.ProcessAction(GenerateReportDateRangePacket("CUSTOM_PROVIDER_REPORT"));
            //Assert.IsNotNull(packet);
            Assert.AreNotEqual(packet, null);
        }

        /// <summary>
        /// Passing the wrong action should throw an exception
        /// </summary>
        [TestMethod]
        public void RequestRequestCustomProvider_Invalid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            Assert.ThrowsException<ArgumentException>(() =>
                server.ProcessAction(GenerateTestMemberPacket("CUSTOM_PROVIDER_REPORT")));
        }

        /// <summary>
        /// Should return a non-null packet
        /// </summary>
        [TestMethod]
        public void RequestRequestCustomMember_Valid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            ResponsePacket packet = server.ProcessAction(GenerateReportDateRangePacket("CUSTOM_MEMBER_REPORT"));
            Assert.IsNotNull(packet);
        }

        /// <summary>
        /// Passing the wrong action should throw an exception
        /// </summary>
        [TestMethod]
        public void RequestRequestCustomMember_Invalid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            Assert.ThrowsException<ArgumentException>(() =>
                server.ProcessAction(GenerateTestMemberPacket("CUSTOM_MEMBER_REPORT")));
        }

        /// <summary>
        /// Testing the main accounting procedure
        /// </summary>
        [TestMethod]
        public void RequestMainAccountingProcedure_Valid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            ResponsePacket packet = server.ProcessAction(
                new BasePacket("MAIN_ACCOUNTING_PROCEDURE", "1234"));
        }

        [TestMethod]
        public void RequestMemberStatus_Valid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            ResponsePacket packet = server.ProcessAction(GenerateTestRequestMemberPacket("MEMBER_STATUS"));
            Assert.IsNotNull(packet);
        }

        [TestMethod]
        public void RequestMemberStatus_Invalid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            Assert.ThrowsException<ArgumentException>(() =>
               server.ProcessAction(GenerateTestInvoicePacket("MEMBER_STATUS")));
        }

        [TestMethod]
        public void RequestProviderDirectory_Valid()
        {
            
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            ResponsePacket packet = server.ProcessAction(
                new BasePacket("VIEW_PROVIDER_DIRECTORY", "1234"));
        }

        [TestMethod]
        public void RequestRemoveMember_Valid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            ResponsePacket packet = server.ProcessAction(GenerateTestMemberPacket("REMOVE_MEMBER"));
        }

        [TestMethod]
        public void RequestRemoveMember_Invalid()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            Assert.ThrowsException<ArgumentException>(() =>
                server.ProcessAction(GenerateTestInvoicePacket("REMOVE_MEMBER")));
        }
    }
}
