using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class ResponsePacketTests
    {
        /// <summary>
        /// Tests the response packet constructor
        /// </summary>
        [TestMethod]
        public void TestResponsePacketConstructor()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string data = "This is super important data >_>'";
            string response = "The member 123456789 was succesfully added.";
            ResponsePacket testPacket = new ResponsePacket(action, sessionID,
                data,response);
            Assert.AreEqual(data, testPacket.Data());
            Assert.AreEqual(response, testPacket.Response());
            return;
        }

    }
}
