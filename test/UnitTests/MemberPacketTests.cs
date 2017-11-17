/*The MemberPacketTests class is for testing all the inputs into each function of
  the MemberPacket class.
*/
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class MemberPacketTests
    {
        /// <summary>
        /// This function checks that all the inputs into the constructor cause the
        /// correct result.
        /// </summary>
        [TestMethod]
        public void MemberPacketConstructor()
        {
            //The happy path takes in a legal action string, session ID, id, status,
            //name, address, city, state, zip, and email and makes sure that all the
            //datamember are correctly initialized.
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
          
            //This test checks whether the ArgumentException gets thrown if a null
            //string is entered for the id string.
            id = null;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));

            //This test checks whether the NullReferenceException gets thrown if a null
            //string is entered for the status string.
            id = "123456789";
            status = null;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));

            //This test checks whether the NullReferenceException gets thrown if a null
            //string is entered for the name string.
            status = "valid";
            name = null;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));

            //This test checks whether the NullReferenceException gets thrown if a null
            //string is entered for the address string.
            name = "John Smith";
            address = null;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));

            //This test checks whether the NullReferenceException gets thrown if a null
            //string is entered for the city string.
            address = "12345 Street Address rd";
            city = null;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));

            //This test checks whether the NullReferenceException gets thrown if a null
            //string is entered for the state string.
            city = "Portland";
            state = null;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));

            //This test checks whether the NullReferenceException gets thrown if a null
            //string is entered for the zip string.
            state = "OR";
            zip = null;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));

            //This test checks whether the NullReferenceException gets thrown if a null
            //string is entered for the email string.
            zip = "97106";
            email = null;
            Assert.ThrowsException<NullReferenceException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));

            //This test checks whether the ArgumentException gets thrown if a the
            //string for id isn't a number.
            id = "Yolo";
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
            
            //This test checks whether the ArgumentOutOfRangeException gets thrown if a the
            //string for the id is a number that is smaller then nine-digit number.
            id = "12345678";
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
            
            //This test checks whether the ArgumentOutOfRangeException gets thrown if a the
            //string for the id is a number that is larger then nine-digit number.
            id = "1234567890";
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new MemberPacket(action, sessionID, id, status,
                name, address, city, state, zip, email));
        }
    }
}
