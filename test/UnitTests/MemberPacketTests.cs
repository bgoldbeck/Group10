using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class MemberPacketTests
    {
        [TestMethod]
        public void MemberPacketConstructor()
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

            MemberPacket testPacket = new MemberPacket(action, sessionID,
        id, status, name, address, city, state, zip, email);
            Assert.AreEqual(id, testPacket.ID());
            Assert.AreEqual(status, testPacket.Status());
            Assert.AreEqual(name, testPacket.Name());
            Assert.AreEqual(address, testPacket.Address());
            Assert.AreEqual(city, testPacket.City());
            Assert.AreEqual(state, testPacket.State());
            Assert.AreEqual(zip, testPacket.Zip());
            Assert.AreEqual(email, testPacket.Email());
        }
        [TestMethod]
        public void NullIDMemberPacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            string id = null;
            string status = "valid";
            string name = "John Smith";
            string address = "12345 Street Address rd";
            string city = "Portland";
            string state = "OR";
            string zip = "97106";
            string email = "Member_Packet@gmail.com";

            MemberPacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
        }
        [TestMethod]
        public void NullStatusMemberPacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            string id = "123456789";
            string status = null;
            string name = "John Smith";
            string address = "12345 Street Address rd";
            string city = "Portland";
            string state = "OR";
            string zip = "97106";
            string email = "Member_Packet@gmail.com";

            MemberPacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
        }
        [TestMethod]
        public void NullNameMemberPacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            string id = "123456789";
            string status = "valid";
            string name = null;
            string address = "12345 Street Address rd";
            string city = "Portland";
            string state = "OR";
            string zip = "97106";
            string email = "Member_Packet@gmail.com";

            MemberPacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
        }
        [TestMethod]
        public void NullAddressMemberPacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            string id = "123456789";
            string status = "valid";
            string name = "John Smith";
            string address = null;
            string city = "Portland";
            string state = "OR";
            string zip = "97106";
            string email = "Member_Packet@gmail.com";

            MemberPacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
        }
        [TestMethod]
        public void NullCityMemberPacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            string id = "123456789";
            string status = "valid";
            string name = "John Smith";
            string address = "12345 Street Address rd";
            string city = null;
            string state = "OR";
            string zip = "97106";
            string email = "Member_Packet@gmail.com";

            MemberPacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
        }
        [TestMethod]
        public void NullStateMemberPacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            string id = "123456789";
            string status = "valid";
            string name = "John Smith";
            string address = "12345 Street Address rd";
            string city = "Portland";
            string state = null;
            string zip = "97106";
            string email = "Member_Packet@gmail.com";

            MemberPacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
        }
        [TestMethod]
        public void NullZipMemberPacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            string id = "123456789";
            string status = "valid";
            string name = "John Smith";
            string address = "12345 Street Address rd";
            string city = "Portland";
            string state = "OR";
            string zip = null;
            string email = "Member_Packet@gmail.com";

            MemberPacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
        }
        [TestMethod]
        public void NullEmailMemberPacketConstructor()
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
            string email = null;

            MemberPacket testPacket;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
        }
        [TestMethod]
        public void IDNotIntMemberPacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            string id = "Yolo";
            string status = "valid";
            string name = "John Smith";
            string address = "12345 Street Address rd";
            string city = "Portland";
            string state = "OR";
            string zip = "97106";
            string email = "Member_Packet@gmail.com";

            MemberPacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
        }
        [TestMethod]
        public void IDTooSmallMemberPacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            string id = "12345678";
            string status = "valid";
            string name = "John Smith";
            string address = "12345 Street Address rd";
            string city = "Portland";
            string state = "OR";
            string zip = "97106";
            string email = "Member_Packet@gmail.com";

            MemberPacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
        }
        [TestMethod]
        public void IDTooLargeMemberPacketConstructor()
        {
            string action = "add member";
            string sessionID = "1234";
            string id = "1234567890";
            string status = "valid";
            string name = "John Smith";
            string address = "12345 Street Address rd";
            string city = "Portland";
            string state = "OR";
            string zip = "97106";
            string email = "Member_Packet@gmail.com";

            MemberPacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
        }
    }
}
