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
            string dateStart = "11-01-2017";
            string dateEnd = "11-07-2017";
            int id = 987654321;
            DateRangePacket testPacket = new DateRangePacket(dateStart, dateEnd, id);
            Assert.AreEqual(dateStart, testPacket.DateStart());
            Assert.AreEqual(dateEnd, testPacket.DateEnd());
            Assert.AreEqual(id, testPacket.ID());
            return;
        }
    }
}
