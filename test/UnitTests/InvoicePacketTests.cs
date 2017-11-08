using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class InvoicePacketTests
    {
        [TestMethod]
        public void TestInvoicePacketConstructor()
        {
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            int providerID = 12345678;
            int memberID = 10000000;
            int serviceCode = 123456;
            string comments = "This customer got a massage for chocolate cramps.";
            InvoicePacket testPacket = new InvoicePacket(currentDateTime,
                dateServiceProvided, providerID, memberID, serviceCode, comments);
            Assert.AreEqual(currentDateTime, testPacket.CurrentDateTime());
            Assert.AreEqual(dateServiceProvided, testPacket.DateServiceProvided());
            Assert.AreEqual(providerID, testPacket.ProviderID());
            Assert.AreEqual(memberID, testPacket.MemberID());
            Assert.AreEqual(serviceCode, testPacket.ServiceCode());
            Assert.AreEqual(comments, testPacket.Comments());
        }
    }
}

