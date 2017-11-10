using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class ProviderPacketTests
    {
        [TestMethod]
        public void ProviderPacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            string id = "123456789";
            string status = "valid";
            string name = "John Smith";
            string address = "12345 Street Address rd";
            string city = "Portland";
            string state = "OR";
            string zip = "97106";
            string email = "Member_Packet@gmail.com";
            string password = "admin";

            ProviderPacket testPacket = new ProviderPacket(action, sessionID,
        id, status, name, address, city, state, zip, email, password);
            Assert.AreEqual(id, testPacket.ID());
            Assert.AreEqual(status, testPacket.Status());
            Assert.AreEqual(name, testPacket.Name());
            Assert.AreEqual(address, testPacket.Address());
            Assert.AreEqual(city, testPacket.City());
            Assert.AreEqual(state, testPacket.State());
            Assert.AreEqual(zip, testPacket.Zip());
            Assert.AreEqual(email, testPacket.Email());
            Assert.AreEqual(password, testPacket.Password());
        }
        [TestMethod]
        public void NullPasswordProviderPacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            string id = "123456789";
            string status = "valid";
            string name = "John Smith";
            string address = "12345 Street Address rd";
            string city = "Portland";
            string state = "OR";
            string zip = "97106";
            string email = "Member_Packet@gmail.com";
            string password = null;

            ProviderPacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new ProviderPacket(action, sessionID, id, status,
                name,address,city,state,zip,email,password));
        }
    }
}
