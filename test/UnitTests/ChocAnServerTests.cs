using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ChocAnServer;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class ChocAnServerTests
    {
        [TestMethod]
        public void TestProcessAction()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            return;
        }

        [TestMethod]
        public void TestRequestAddMember()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();
            return;
        }

        [TestMethod]
        public void TestRequestMemberStatus()
        {
            ChocAnServer.ChocAnServer server = new ChocAnServer.ChocAnServer();

            //server.ProcessAction(new MemberPacket(1234, "MEMBER_STATUS"));

            return;
        }
    }
}
