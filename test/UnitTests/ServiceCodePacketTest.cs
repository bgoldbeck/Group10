﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class ServiceCodePacketTests
    {
        /// <summary>
        /// Tests the service code packet constructor
        /// </summary>
        [TestMethod]
        public void ServiceCodePacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            float fee = 250;
            string id = "123456";
            string name = "John Smith";
            string providerID = "123456";

            ServiceCodePacket testPacket = new ServiceCodePacket(action, sessionID, providerID,
        fee,id,name);
            Assert.AreEqual(fee, testPacket.Fee());
            Assert.AreEqual(id, testPacket.ID());
            Assert.AreEqual(name, testPacket.Name());
        }

        /// <summary>
        /// Tests running a null ID thru the service code packet constructor
        /// </summary>
        [TestMethod]
        public void NullIDServiceCodePacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            float fee = 250;
            string id = null;
            string name = "John Smith";
            string providerID = "123456";

            ServiceCodePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new ServiceCodePacket(action, sessionID, providerID,
            fee, id, name));
        }

        /// <summary>
        /// Tests a null name in the service code packet constructor
        /// </summary>
        [TestMethod]
        public void NullNameServiceCodePacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            float fee = 250;
            string id = "123456";
            string name = null;
            string providerID = "123456";

            ServiceCodePacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new ServiceCodePacket(action, sessionID, providerID,
            fee, id, name));
        }
        
        /// <summary>
        /// Tests a negative fee in the service code packet constructor
        /// </summary>
        [TestMethod]
        public void NegativeFeeServiceCodePacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            float fee = -250;
            string id = "123456";
            string name = "John Smith";
            string providerID = "123456";

            ServiceCodePacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new ServiceCodePacket(action, sessionID, providerID,
            fee, id, name));
        }
        [TestMethod]
        public void IDNotIntServiceCodePacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            float fee = 250;
            string id = "Choc Therapy";
            string name = "John Smith";
            string providerID = "123456";

            ServiceCodePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new ServiceCodePacket(action, sessionID, providerID,
            fee, id, name));
        }
        [TestMethod]
        public void IDTooSmallServiceCodePacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            float fee = -250;
            string id = "12345";
            string name = "John Smith";
            string providerID = "123456";

            ServiceCodePacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new ServiceCodePacket(action, sessionID, providerID,
            fee, id, name));
        }
        [TestMethod]
        public void IDTooLargeServiceCodePacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            float fee = -250;
            string id = "1234567";
            string name = "John Smith";
            string providerID = "123456";

            ServiceCodePacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new ServiceCodePacket(action, sessionID, providerID,
            fee, id, name));
        }
    }
}
