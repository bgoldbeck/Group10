/*The DateRangePacketTests class is for testing all the inputs into each function of
  the DateRangePacket class.
*/
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class DateRangePacketTests
    {
        /// <summary>
        /// This function checks that all the inputs into the constructor cause the
        /// correct result.
        /// </summary>
        [TestMethod]
        public void TestDateRangePacketConstructor()
        {
            //The happy path takes in a legal action string, session ID, dateStart,
            //dateEnd, and id and it checks to make sure the values are correctly 
            //set in the packet.
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

            //This test checks whether the ArgumentException gets thrown if a null
            //string is entered for the dateStart string.
            dateStart = null;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));

            //This test checks whether the ArgumentException gets thrown if a null
            //string is entered for the dateEnd string.
            dateStart = "11-01-2017";
            dateEnd = null;
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));

            //This test checks whether the ArgumentException gets thrown if a null
            //string is entered for the id string.
            dateEnd = "11-07-2017";
            id = null;
            //This one is an argumentException because it fails to parse.
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));

            //This test checks whether the ArgumentException gets thrown if a the
            //string for the id isn't a number.
            id = "Member #42";
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));

            //This test checks whether the ArgumentOutOfRangeException gets thrown if a the
            //string for the id is a number that is smaller then nine-digit number.
            id = "42";
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));

            //This test checks whether the ArgumentOutOfRangeException gets thrown if a the
            //string for the id is a number that is larger then nine-digit number.
            id = "1000000000";
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));

            //This test checks whether the ArgumentException gets thrown if a the
            //string for the dateStart isn't a legal date.
            dateStart = "hi";
            id = "987654321";
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));

            //This test checks whether the ArgumentException gets thrown if a the
            //string for the dateEnd isn't a legal date.
            dateStart = "11-01-2017";
            dateEnd = "hi";
            Assert.ThrowsException<ArgumentException>(
                () => testPacket = new DateRangePacket(action, sessionID,
                dateStart, dateEnd, id));
            return;
        }
    }
}
