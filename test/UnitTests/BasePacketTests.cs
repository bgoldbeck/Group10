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
    }
}
