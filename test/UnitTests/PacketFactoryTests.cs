using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthcareClientSystem.IO;

namespace UnitTests
{
    [TestClass]
    public class PacketFactoryTests
    {
        [TestMethod]
        public void PacketFactory_BuildPacket_NullTextUI()
        {
            PacketFactory packetFactory = new PacketFactory();
            Assert.ThrowsException<ArgumentNullException>(
                () => packetFactory.BuildPacket(null, "", ""));
        }

        [TestMethod]
        public void PacketFactory_BuildPacket_InvalidPacketType()
        {
            TextUI textUI = new TextUI();
            PacketFactory packetFactory = new PacketFactory();
            Assert.ThrowsException<ArgumentException>(
                () => packetFactory.BuildPacket(textUI, "badPacket", ""));
        }
    }
}
