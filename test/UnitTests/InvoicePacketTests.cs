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
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "123456789";
            string memberID = "100000000";
            string serviceCode = "123456";
            string comments = "This customer got a massage for chocolate cramps.";
            InvoicePacket testPacket = new InvoicePacket(action, sessionID,
                currentDateTime,dateServiceProvided, providerID, memberID, 
                serviceCode, comments);
            Assert.AreEqual(currentDateTime, testPacket.CurrentDateTime());
            Assert.AreEqual(dateServiceProvided, testPacket.DateServiceProvided());
            Assert.AreEqual(providerID, testPacket.ProviderID());
            Assert.AreEqual(memberID, testPacket.MemberID());
            Assert.AreEqual(serviceCode, testPacket.ServiceCode());
            Assert.AreEqual(comments, testPacket.Comments());
        }
        [TestMethod]
        public void NullCurrentDateInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = null;
            string dateServiceProvided = "11-06-2017";
            string providerID = "123456789";
            string memberID = "100000000";
            string serviceCode = "123456";
            string comments = "This customer got a massage for chocolate cramps.";
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void NullDateProvidedInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = null;
            string providerID = "123456789";
            string memberID = "100000000";
            string serviceCode = "123456";
            string comments = "This customer got a massage for chocolate cramps.";
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void NullProviderIDInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = null;
            string memberID = "100000000";
            string serviceCode = "123456";
            string comments = "This customer got a massage for chocolate cramps.";
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void NullMemberIDInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "123456789";
            string memberID = null;
            string serviceCode = "123456";
            string comments = "This customer got a massage for chocolate cramps.";
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void NullServiceCodeInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "123456789";
            string memberID = "100000000";
            string serviceCode = null;
            string comments = "This customer got a massage for chocolate cramps.";
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void NullCommentsInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "123456789";
            string memberID = "100000000";
            string serviceCode = "123456";
            string comments = null;
            InvoicePacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void ProviderIDNotIntInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "Provider #42";
            string memberID = "100000000";
            string serviceCode = "123456";
            string comments = null;
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void MemberIDNotIntInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "123456789";
            string memberID = "Member #42";
            string serviceCode = "123456";
            string comments = null;
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void ServiceCodeNotIntInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "123456789";
            string memberID = "100000000";
            string serviceCode = "Choc Therapy";
            string comments = null;
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void ProviderIDTooSmallInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "42";
            string memberID = "100000000";
            string serviceCode = "123456";
            string comments = null;
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void ProviderIDTooLargeInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "1000000000";
            string memberID = "100000000";
            string serviceCode = "123456";
            string comments = null;
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void MemberIDTooSmallInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "100000000";
            string memberID = "13";
            string serviceCode = "123456";
            string comments = null;
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void MemberIDTooLargeInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "100000000";
            string memberID = "1000000000";
            string serviceCode = "123456";
            string comments = null;
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void ServiceCodeTooSmallInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "100000000";
            string memberID = "100000000";
            string serviceCode = "1";
            string comments = null;
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
        [TestMethod]
        public void ServiceCodeTooLargeInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string currentDateTime = "11-07-2017 17:58:17";
            string dateServiceProvided = "11-06-2017";
            string providerID = "100000000";
            string memberID = "100000000";
            string serviceCode = "1234567";
            string comments = null;
            InvoicePacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
    }
}

