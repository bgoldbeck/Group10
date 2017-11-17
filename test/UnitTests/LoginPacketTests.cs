/*The LoginPacketTests class is for testing all the inputs into each function of
  the LoginPacket class.
*/
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class LoginPacketTests
    {
        /// <summary>
        /// This function checks that all the inputs into the constructor cause the
        /// correct result.
        /// </summary>
        [TestMethod]
        public void TestInvoicePacketConstructor()
        {
            //The happy path takes in a legal action string, session ID, userID, and
            //password and checks to make sure that they are initialized correctly.
            string action = "Add Member";
            string sessionID = "1209384209385";
            string userID = "123456789";
            string password = "Admin";
            LoginPacket testPacket = new LoginPacket(action, sessionID,
            userID, password,0);
            Assert.AreEqual(userID, testPacket.ID());
            Assert.AreEqual(password, testPacket.Password());

            //This test checks whether the ArgumentException gets thrown if a null
            //string is entered for the userID string.
            userID = null;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new LoginPacket(action, sessionID,
                userID, password,0));

            //This test checks whether the ArgumentException gets thrown if a null
            //string is entered for the userID string.
            userID = "123456789";
            password = null;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new LoginPacket(action, sessionID,
                userID, password,0));

            //This test checks whether the ArgumentOutOfRangeException gets thrown if a the
            //string for the user id is a number that is smaller then nine-digit number.
            userID = "12345678";
            password = "Admin";
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new LoginPacket(action, sessionID,
                userID, password,0));

            //This test checks whether the ArgumentOutOfRangeException gets thrown if a the
            //string for the user id is a number that is larger then nine-digit number.
            userID = "1234567890";
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new LoginPacket(action, sessionID,
                userID, password,0));
        }
    }
}
