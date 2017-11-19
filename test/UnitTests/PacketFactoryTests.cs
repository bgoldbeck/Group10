using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthcareClientSystem.IO;

namespace UnitTests
{
    [TestClass]
    public class PacketFactoryTests
    {

        private String MemberID = "123456789";
        private String MemberName = "SampleMember";
        private String MemberAddress = "12345 Address";
        private String MemberCity = "Sample City";
        private String MemberState = "OR";
        private String MemberZip = "12345";
        private String MemberEmail = "12345@1234.com";

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
            TextUI textUI = new TextUI(true);
            PacketFactory packetFactory = new PacketFactory();
            Assert.ThrowsException<ArgumentException>(
                () => packetFactory.BuildPacket(textUI, "badPacket", ""));
        }

        [TestMethod]
        public void PacketFactory_BuildPacket_MemberPacket_Normal()
        {
            InputController.EnableMock();
            InputController.AddMockInput(MemberID);
            InputController.AddMockInput("N");
            InputController.AddMockInput(MemberName);
            InputController.AddMockInput(MemberAddress);
            InputController.AddMockInput(MemberCity);
            InputController.AddMockInput(MemberState);
            InputController.AddMockInput(MemberZip);
            InputController.AddMockInput(MemberEmail);


            TextUI textUI = new TextUI(true);
            PacketFactory packetFactory = new PacketFactory();

            var result = packetFactory.BuildPacket(textUI, "MemberPacket", "UPDATE_MEMBER");

            Assert.IsNotNull(result);
            InputController.DisableMock();
        }
    }
}
