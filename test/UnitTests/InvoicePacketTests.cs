/*The InvoicePacketTests class is for testing all the inputs into each function of
  the InvoicePacket class.
*/
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class InvoicePacketTests
    {
        /// <summary>
        /// This function checks that all the inputs into the constructor cause the
        /// correct result.
        /// </summary>
        [TestMethod]
        public void TestInvoicePacketConstructor()
        {
            //The happy path takes in a legal action string, session ID, dateStart,
            //dateEnd, and id and it checks to make sure the values are correctly 
            //set in the packet.
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

            //This test checks whether the ArgumentException gets thrown if a null
            //string is entered for the currentDateTime string.
            currentDateTime = null;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentException gets thrown if a null
            //string is entered for the dateServiceProvided string.
            currentDateTime = "11-07-2017 17:58:17";
            dateServiceProvided = null;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentException gets thrown if a null
            //string is entered for the providerID string.
            dateServiceProvided = "11-06-2017";
            providerID = null;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentException gets thrown if a null
            //string is entered for the memberID string.
            providerID = "123456789";
            memberID = null;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentException gets thrown if a null
            //string is entered for the serviceCode string.
            memberID = "100000000";
            serviceCode = null;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the NullReferenceException gets thrown if a null
            //string is entered for the comments string.
            serviceCode = "123456";
            comments = null;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentException gets thrown if a the
            //string for the provider id isn't a number.
            providerID = "Provider #42";
            comments = "This customer got a massage for chocolate cramps.";
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentException gets thrown if a the
            //string for the member id isn't a number.
            providerID = "123456789";
            memberID = "Member #42";
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentException gets thrown if a the
            //string for the service code id isn't a number.
            memberID = "100000000";
            serviceCode = "Choc Therapy";
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentOutOfRangeException gets thrown if a the
            //string for the provider id is a number that is smaller then nine-digit number.
            providerID = "42";
            serviceCode = "123456";
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentOutOfRangeException gets thrown if a the
            //string for the provider id is a number that is larger then nine-digit number.
            providerID = "1000000000";
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentOutOfRangeException gets thrown if a the
            //string for the member id is a number that is smaller then nine-digit number.
            providerID = "100000000";
            memberID = "13";
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentOutOfRangeException gets thrown if a the
            //string for the member id is a number that is larger then nine-digit number.
            memberID = "1000000000";
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentOutOfRangeException gets thrown if a the
            //string for the service code id is a number that is smaller then nine-digit number.
            memberID = "100000000";
            serviceCode = "1";
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));

            //This test checks whether the ArgumentOutOfRangeException gets thrown if a the
            //string for the service code id is a number that is larger then nine-digit number.
            serviceCode = "1234567";
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new InvoicePacket(action, sessionID,
                currentDateTime, dateServiceProvided, providerID, memberID,
                serviceCode, comments));
            return;
        }
    }
}

