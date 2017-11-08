using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class BasePacketTests
    {
        [TestMethod]
        public void TestBasePacketConstructor()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            BasePacket testPacket = new BasePacket(action, sessionID);
            Assert.AreEqual(action, testPacket.Action());
            Assert.AreEqual(sessionID, testPacket.SessionID());
            return;
        }

        [TestMethod]
        public void TestNullActionBasePacket()
        {
            string action = null;
            string sessionID = "1209384209385";
            BasePacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new BasePacket(action, sessionID));
            return;
        }
        [TestMethod]
        public void TestNullSessionBasePacket()
        {
            string action = "Add Member";
            string sessionID = null;
            BasePacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new BasePacket(action, sessionID));
            return;
        }
    }
}
