using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class DateRangePacketTests
    {
        [TestMethod]
        public void TestDateRangePacketConstructor()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string dateStart = "11-01-2017";
            string dateEnd = "11-07-2017";
            string id = "987654321";
            DateRangePacket testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id);
            Assert.AreEqual(dateStart, testPacket.DateStart());
            Assert.AreEqual(dateEnd, testPacket.DateEnd());
            Assert.AreEqual(id, testPacket.ID());
            return;
        }
        [TestMethod]
        public void NullDateStartDateRangePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string dateStart = null;
            string dateEnd = "11-07-2017";
            string id = "987654321";
            DateRangePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));
            return;
        }
        [TestMethod]
        public void NullDateEndDateRangePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string dateStart = "11-01-2017";
            string dateEnd = null;
            string id = "987654321";
            DateRangePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));
            return;
        }
        [TestMethod]
        public void NullIDDateRangePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string dateStart = "11-01-2017";
            string dateEnd = "11-07-2017";
            string id = null;
            DateRangePacket testPacket;
            //This one is an argumentException because it fails to parse.
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));
            return;
        }
        [TestMethod]
        public void IDNotIntDateRangePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string dateStart = "11-01-2017";
            string dateEnd = "11-07-2017";
            string id = "Member #42";
            DateRangePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));
            return;
        }
        [TestMethod]
        public void IDTooSmallDateRangePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string dateStart = "11-01-2017";
            string dateEnd = "11-07-2017";
            string id = "42";
            DateRangePacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));
            return;
        }
        [TestMethod]
        public void IDTooLargeDateRangePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string dateStart = "11-01-2017";
            string dateEnd = "11-07-2017";
            string id = "1000000000";
            DateRangePacket testPacket;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));
            return;
        }
        [TestMethod]
        public void InvalidStartDateDateRangePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string dateStart = "hi";
            string dateEnd = "11-07-2017";
            string id = "987654321";
            DateRangePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));
            return;
        }
        [TestMethod]
        public void InvalidEndDateDateRangePacket()
        {
            string action = "Add Member";
            string sessionID = "1209384209385";
            string dateStart = "11-01-2017";
            string dateEnd = "hi";
            string id = "987654321";
            DateRangePacket testPacket;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));
            return;
        }
    }
}
