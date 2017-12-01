
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    /// <summary>
    /// The BasePacketTests class is for testing all the inputs into each
    /// function of the BasePacket class.
    /// </summary>
    [TestClass]
    public class BasePacketTests
    {
        /// <summary>
        /// This function checks that all the inputs into the constructor cause the
        /// correct result.
        /// </summary>
        [TestMethod]
        public void TestBasePacketConstructor()
        {
            //The happy path takes in a legal action string and session ID and it
            //checks to make sure the values are correctly set in the packet.
            string action = "Add Member";
            string sessionID = "1209384209385";
            BasePacket testPacket = new BasePacket(action, sessionID);
            Assert.AreEqual(action, testPacket.Action());
            Assert.AreEqual(sessionID, testPacket.SessionID());

            //This test checks whether the NullReferenceException gets thrown if a null
            //string is entered for the action string.
            action = null;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new BasePacket(action, sessionID));

            //This test checks whether the NullReferenceException gets thrown if a
            //string is entered for the sessionID string.
            action = "Add Member";
            sessionID = null;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new BasePacket(action, sessionID));
            return;
        }
    }
}
