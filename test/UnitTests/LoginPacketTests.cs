using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class LoginPacketTests
    {
        [TestMethod]
        public void TestInvoicePacketConstructor()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string userID = "123456789";
            string password = "Admin";

            LoginPacket testPacket = new LoginPacket(action, sessionID,
            userID, password,0);
            Assert.AreEqual(userID, testPacket.ID());
            Assert.AreEqual(password, testPacket.Password());
        }
        [TestMethod]
        public void NullIDInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string userID = null;
            string password = "Admin";

            LoginPacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new LoginPacket(action, sessionID,
                userID, password,0));
        }
        [TestMethod]
        public void NullPasswordInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string userID = "123456789";
            string password = null;

            LoginPacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new LoginPacket(action, sessionID,
                userID, password,0));
        }
        [TestMethod]
        public void IDTooSmallInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string userID = "12345678";
            string password = null;

            LoginPacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new LoginPacket(action, sessionID,
                userID, password,0));
        }
        [TestMethod]
        public void IDTooLargeInvoicePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string userID = "1234567890";
            string password = null;

            LoginPacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new LoginPacket(action, sessionID,
                userID, password,0));
        }
    }
}
