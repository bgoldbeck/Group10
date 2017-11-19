using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthcareClientSystem.IO;

namespace UnitTests
{
    [TestClass]
    public class PacketFactoryTests
    {

        private String SampleID = "123456789";
        private String SampleCode = "123456";
        private String SampleDate = "12-12-2016";
        private String SampleName = "SampleMember";
        private String SampleAddress = "12345 Address";
        private String SampleCity = "Sample City";
        private String SampleState = "OR";
        private String SampleZip = "12345";
        private String SampleEmail = "12345@1234.com";
        public string SamplePassword = "password";
        private string SampleFee = "45.67";

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
            InputController.AddMockInput(SampleID);
            InputController.AddMockInput("N");
            InputController.AddMockInput(SampleName);
            InputController.AddMockInput(SampleAddress);
            InputController.AddMockInput(SampleCity);
            InputController.AddMockInput(SampleState);
            InputController.AddMockInput(SampleZip);
            InputController.AddMockInput(SampleEmail);


            TextUI textUI = new TextUI(true);
            PacketFactory packetFactory = new PacketFactory();

            var result = packetFactory.BuildPacket(textUI, "MemberPacket", "UPDATE_MEMBER");

            Assert.IsNotNull(result);
            InputController.DisableMock();
        }

        [TestMethod]
        public void PacketFactory_BuildPacket_ProviderPacket_Normal()
        {
            InputController.EnableMock();
            InputController.AddMockInput(SampleID);
            InputController.AddMockInput("N");
            InputController.AddMockInput(SampleName);
            InputController.AddMockInput(SampleAddress);
            InputController.AddMockInput(SampleCity);
            InputController.AddMockInput(SampleState);
            InputController.AddMockInput(SampleZip);
            InputController.AddMockInput(SampleEmail);
            InputController.AddMockInput(SamplePassword);

            TextUI textUI = new TextUI(true);
            PacketFactory packetFactory = new PacketFactory();

            var result = packetFactory.BuildPacket(textUI, "ProviderPacket", "UPDATE_PROVIDER");

            Assert.IsNotNull(result);
            InputController.DisableMock();
        }

        [TestMethod]
        public void PacketFactory_BuildPacket_InvoicePacket_Normal()
        {
            InputController.EnableMock();
            InputController.AddMockInput(SampleDate);
            InputController.AddMockInput(SampleID);
            InputController.AddMockInput(SampleCode);
            InputController.AddMockInput("comments");

            TextUI textUI = new TextUI(true);
            PacketFactory packetFactory = new PacketFactory();

            var result = packetFactory.BuildPacket(textUI, "InvoicePacket", "", "100000001", "100000001");

            Assert.IsNotNull(result);
            InputController.DisableMock();
        }

        [TestMethod]
        public void PacketFactory_BuildPacket_ServiceCodePacket_Normal()
        {
            InputController.EnableMock();
            InputController.AddMockInput(SampleCode);
            InputController.AddMockInput(SampleID);
            InputController.AddMockInput(SampleName);
            InputController.AddMockInput(SampleFee);

            TextUI textUI = new TextUI(true);
            PacketFactory packetFactory = new PacketFactory();

            var result = packetFactory.BuildPacket(textUI, "ServiceCodePacket", "UPDATE_SERVICE_CODE", "100000001");

            Assert.IsNotNull(result);
            InputController.DisableMock();
        }

        [TestMethod]
        public void PacketFactory_BuildPacket_LoginPacket_Normal()
        {
            InputController.EnableMock();
            InputController.AddMockInput(SampleID);
            InputController.AddMockInput(SamplePassword);

            TextUI textUI = new TextUI(true);
            PacketFactory packetFactory = new PacketFactory();

            var result = packetFactory.BuildPacket(textUI, "LoginPacket", "", "100000001");

            Assert.IsNotNull(result);
            InputController.DisableMock();
        }

        [TestMethod]
        public void PacketFactory_BuildPacket_DateRangePacket_Normal()
        {
            InputController.EnableMock();
            InputController.AddMockInput(SampleID);

            TextUI textUI = new TextUI(true);
            PacketFactory packetFactory = new PacketFactory();

            var result = packetFactory.BuildPacket(textUI, "DateRangePacket", "", "100000001");

            Assert.IsNotNull(result);
            InputController.DisableMock();
        }
    }
}
